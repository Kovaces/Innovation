using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Translation : CardBase
    {
        public override string Name => "Translation";
        public override int Age => 3;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Crown,"You may meld all the cards in your score pile. If you meld one, you must meld them all.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Crown,"If each top card on your board has a [CROWN], claim the World achievement.", Action2)
        };


        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Tableau.ScorePile.Any())
                return;

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may meld all the cards in your score pile. If you meld one, you must meld them all.");
            if (!answer.HasValue || !answer.Value)
                return;

            PlayerActed(parameters);

            var cardsToMeld = parameters.TargetPlayer.Interaction.OrderCards(parameters.TargetPlayer.Id, parameters.TargetPlayer.Tableau.ScorePile);

            foreach (var card in cardsToMeld)
            {
                parameters.TargetPlayer.RemoveCardFromScorePile(card);
                Meld.Action(card, parameters.TargetPlayer);
            }
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Tableau.GetTopCards().Any())
                return;

            if (parameters.TargetPlayer.Tableau.GetTopCards().Any(c => !c.HasSymbol(Symbol.Crown)))
                return;
            
            throw new NotImplementedException("World Achievement"); // TODO::achieve World.  Special achievements need a larger framework and some discussion
            PlayerActed(parameters);
        }
    }
}