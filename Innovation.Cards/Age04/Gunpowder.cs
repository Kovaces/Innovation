using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Gunpowder : CardBase
    {
        public override string Name => "Gunpowder";
        public override int Age => 4;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a top card with a [TOWER] from your board to my score pile!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Factory,"If any card was transferred due to the demand, draw and score a [2].", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //"I demand you transfer a top card with a [TOWER] from your board to my score pile!"
            var topTowerCards = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Tower)).ToList();

            if (topTowerCards.Any())
            {
                var cardToTransfer = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                    new PickCardParameters
                                                                                    {
                                                                                        CardsToPickFrom = topTowerCards,
                                                                                        MinimumCardsToPick = 1,
                                                                                        MaximumCardsToPick = 1
                                                                                    }).First();

                parameters.TargetPlayer.RemoveCardFromStack(cardToTransfer);
                parameters.ActivePlayer.AddCardToScorePile(cardToTransfer);
                
                parameters.AddToStorage("Gunpowder.CardTransferred.Key", true);
            }
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //If any card was transferred due to the demand, draw and score a [2].
            var cardTransfered = parameters.GetFromStorage("Gunpowder.CardTransferred.Key");
            if (cardTransfered != null && !(bool)cardTransfered)
                return;

            Score.Action(Draw.Action(2, parameters.AgeDecks), parameters.TargetPlayer);
        }
    }
}