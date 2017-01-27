using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Canning : CardBase
    {
        public override string Name => "Canning";
        public override int Age => 6;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Factory,"You may draw and tuck a [6]. If you do, score all your top cards without a [FACTORY]", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your yellow cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may draw and tuck a [6]. If you do, score all your top cards without a [FACTORY]");
            if (!answer.HasValue || !answer.Value)
                return;

            PlayerActed(parameters);

            Tuck.Action(Draw.Action(6, parameters.AgeDecks), parameters.TargetPlayer);

            var topCardsToScore = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => !c.HasSymbol(Symbol.Factory)).ToList();

            foreach (var card in topCardsToScore)
            {
                parameters.TargetPlayer.RemoveCardFromStack(card);
                Score.Action(card, parameters.TargetPlayer);
            }
        }

        void Action2(ICardActionParameters parameters)
        {
            AskToSplay(parameters, Color.Yellow, SplayDirection.Right);
        }

        
    }
}