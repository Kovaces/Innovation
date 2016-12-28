using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class ThePirateCode : CardBase
    {
        public override string Name => "The Pirate Code";
        public override int Age => 5;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer two cards of value [4] or less from your score pile to my score pile!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Crown,"If any card was transferred due to the demand, score the lowest top card with a [CROWN] from your board.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Tableau.ScorePile.Any(c => c.Age < 4))
                return;

            var cardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile.Where(c => c.Age < 4).ToList();

            var selectedCards = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                new PickCardParameters
                                                                                {
                                                                                    CardsToPickFrom = cardsToPickFrom,
                                                                                    MinimumCardsToPick = 1,
                                                                                    MaximumCardsToPick = 2
                                                                                }).ToList();

            foreach (var selectedCard in selectedCards)
            {
                parameters.TargetPlayer.RemoveCardFromScorePile(selectedCard);
                parameters.ActivePlayer.AddCardToScorePile(selectedCard);
            }

            parameters.AddToStorage("PirateCodeCardTransfered", true);
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!(bool) parameters.GetFromStorage("PirateCodeCardTransfered"))
                return;
            
            var topCardsWithCrown = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Crown)).ToList();

            if (!topCardsWithCrown.Any())
                return;

            var cardsToChooseFrom = topCardsWithCrown.Where(c => c.Age == topCardsWithCrown.Min(x => x.Age)).ToList();

            var cardToScore = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                            new PickCardParameters
                                                                            {
                                                                                CardsToPickFrom = cardsToChooseFrom,
                                                                                MinimumCardsToPick = 1,
                                                                                MaximumCardsToPick = 1
                                                                            }).First();

            parameters.TargetPlayer.RemoveCardFromStack(cardToScore);
            Score.Action(cardToScore, parameters.TargetPlayer);

            PlayerActed(parameters);
        }
    }
}