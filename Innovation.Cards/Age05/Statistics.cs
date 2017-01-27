using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Statistics : CardBase
    {
        public override string Name => "Statistics";
        public override int Age => 5;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you transfer the highest card in your score pile to your hand! If you do, and have only one card in your hand afterwards, repeat this demand!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may splay your yellow cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Tableau.ScorePile.Any())
                return;

            do
            {
                var cardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile.Where(c => c.Age == parameters.TargetPlayer.Tableau.ScorePile.Max(x => x.Age)).ToList();

                var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, 
                                                                                    new PickCardParameters
                                                                                    {
                                                                                        CardsToPickFrom = cardsToPickFrom,
                                                                                        MinimumCardsToPick = 1,
                                                                                        MaximumCardsToPick = 1
                                                                                    }).First();

                parameters.TargetPlayer.RemoveCardFromScorePile(selectedCard);
                parameters.TargetPlayer.AddCardToHand(selectedCard);

            } while (parameters.TargetPlayer.Hand.Count == 1);
        }

        void Action2(ICardActionParameters parameters)
        {
            AskToSplay(parameters, Color.Yellow, SplayDirection.Right);
        }
    }
}