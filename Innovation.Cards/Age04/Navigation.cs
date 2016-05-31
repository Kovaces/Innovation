using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Navigation : CardBase
    {
        public override string Name => "Navigation";
        public override int Age => 4;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a [2] or [3] from your score pile, if it has any, to my score pile!", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //I demand you transfer a [2] or [3] from your score pile, if it has any, to my score pile!
            var scorePileCards = parameters.TargetPlayer.Tableau.ScorePile.Where(c => c.Age == 2 || c.Age == 3).ToList();
            if (!scorePileCards.Any())
                return;

            var cardToTransfer = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                    new PickCardParameters
                                                                                    {
                                                                                        CardsToPickFrom = scorePileCards,
                                                                                        MinimumCardsToPick = 1,
                                                                                        MaximumCardsToPick = 1
                                                                                    }).First();

            parameters.TargetPlayer.RemoveCardFromScorePile(cardToTransfer);
            parameters.ActivePlayer.AddCardToScorePile(cardToTransfer);
        }
    }
}