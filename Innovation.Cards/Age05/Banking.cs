using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Banking : CardBase
    {
        public override string Name => "Banking";
        public override int Age => 5;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-green card with a [FACTORY] from your board to my board. If you do, draw and score a [5]!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var topCardsWithFactories = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Factory) && c.Color != Color.Green).ToList();

            if (!topCardsWithFactories.Any())
                return;

            var cardToMove = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = topCardsWithFactories, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

            parameters.TargetPlayer.Tableau.Stacks[cardToMove.Color].RemoveCard(cardToMove);

            parameters.ActivePlayer.Tableau.Stacks[cardToMove.Color].AddCardToTop(cardToMove);

            Score.Action(Draw.Action(5, parameters.AgeDecks), parameters.TargetPlayer);
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your green cards right.");
            if (!answer.HasValue || !answer.Value)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.SplayStack(Color.Green, SplayDirection.Right);
        }
    }
}