using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Anatomy : CardBase
    {
        public override string Name => "Anatomy";
        public override int Age => 4;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return a card from your score pile! If you do, return a top card of equal value from your board!", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //"I demand you return a card from your score pile!
            if (parameters.TargetPlayer.Tableau.ScorePile.Any())
            {
                var scoreCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                new PickCardParameters
                                                                {
                                                                    CardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile,
                                                                    MinimumCardsToPick = 1,
                                                                    MaximumCardsToPick = 1
                                                                }).First();

                parameters.TargetPlayer.RemoveCardFromScorePile(scoreCard);
                Return.Action(scoreCard, parameters.AgeDecks);

                //If you do, return a top card of equal value from your board!"
                var topCardsOfSameAge = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.Age.Equals(scoreCard.Age)).ToList();
                var topCard = topCardsOfSameAge.FirstOrDefault();

                if (topCardsOfSameAge.Count > 1)
                {
                    topCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, 
                                                                                new PickCardParameters
                                                                                {
                                                                                    CardsToPickFrom = topCardsOfSameAge,
                                                                                    MinimumCardsToPick = 1,
                                                                                    MaximumCardsToPick = 1
                                                                                }).First();
                }

                if (topCard != null)
                {
                    parameters.TargetPlayer.RemoveCardFromStack(topCard);
                    Return.Action(topCard, parameters.AgeDecks);
                }
            }
        }
    }
}