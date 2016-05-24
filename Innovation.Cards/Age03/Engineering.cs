using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Engineering : CardBase
    {
        public override string Name => "Engineering";
        public override int Age => 3;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Tower;
        public override Symbol Left => Symbol.Blank;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer all top cards with a [TOWER] from your board to my score pile!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Tower,"You may splay your red cards left.", Action2)
        };


        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var towerCards = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Tower)).ToList();
            foreach (var card in towerCards)
            {
                parameters.TargetPlayer.RemoveCardFromStack(card);
                parameters.ActivePlayer.AddCardToScorePile(card);
            }
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your red cards left.");
            if (!answer.HasValue || !answer.Value)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.SplayStack(Color.Red, SplayDirection.Left);
        }
    }
}