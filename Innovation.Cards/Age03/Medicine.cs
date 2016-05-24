using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Medicine : CardBase
    {
        public override string Name => "Medicine";
        public override int Age => 3;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange the highest card in your score pile with the lowest card in my score pile!", Action1)
        };


        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            ICard yourSelectedCard = null;
            ICard mySelectedCard = null;

            var yourHighestScoreCards = parameters.TargetPlayer.Tableau.ScorePile.Where(c => c.Age.Equals(parameters.TargetPlayer.Tableau.ScorePile.Max(d => d.Age))).ToList();
            var myHighestScoreCards = parameters.ActivePlayer.Tableau.ScorePile.Where(c => c.Age.Equals(parameters.ActivePlayer.Tableau.ScorePile.Max(d => d.Age))).ToList();

            if (yourHighestScoreCards.Any())
            {
                yourSelectedCard = yourHighestScoreCards.First();
                
                if (yourHighestScoreCards.Count > 1)
                {
                    yourSelectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = yourHighestScoreCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
                }
            }

            if (myHighestScoreCards.Any())
            {
                mySelectedCard = myHighestScoreCards.First();

                if (myHighestScoreCards.Count > 1)
                {
                    mySelectedCard = parameters.ActivePlayer.Interaction.PickCards(parameters.ActivePlayer.Id, new PickCardParameters { CardsToPickFrom = myHighestScoreCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
                }
            }

            if ((yourSelectedCard == null) && (mySelectedCard == null))
                return;

            if (yourSelectedCard != null)
            {
                parameters.TargetPlayer.RemoveCardFromScorePile(yourSelectedCard);
                parameters.ActivePlayer.AddCardToScorePile(yourSelectedCard);
            }

            if (mySelectedCard != null)
            {
                parameters.ActivePlayer.RemoveCardFromScorePile(mySelectedCard);
                parameters.TargetPlayer.AddCardToScorePile(mySelectedCard);
            }
        }
    }
}