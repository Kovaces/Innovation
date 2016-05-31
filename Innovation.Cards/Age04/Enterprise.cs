using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Enterprise : CardBase
    {
        public override string Name => "Enterprise";
        public override int Age => 4;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-purple card with a [CROWN] from your board to my board! If you do, draw and meld a [4]!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
        };

        private void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //I demand you transfer a top non-purple card with a [CROWN] from your board to my board! 
            var topCrownCards = parameters.TargetPlayer.Tableau.GetTopCards()
                                          .Where(c => c.HasSymbol(Symbol.Crown) && c.Color != Color.Purple)
                                          .ToList();
            if (topCrownCards.Any())
            {
                var cardToTransfer = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                    new PickCardParameters
                                                                                    {
                                                                                        CardsToPickFrom = topCrownCards,
                                                                                        MinimumCardsToPick = 1,
                                                                                        MaximumCardsToPick = 1
                                                                                    }).First();

                parameters.TargetPlayer.RemoveCardFromStack(cardToTransfer);
                parameters.ActivePlayer.AddCardToStack(cardToTransfer);

                //If you do, draw and meld a [4]!
                Meld.Action(Draw.Action(4, parameters.AgeDecks), parameters.TargetPlayer);
            }
        }

        private void Action2(ICardActionParameters parameters)
        {
            //"You may splay your green cards right."
            var response = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "Do you wish to splay your GREEN cards RIGHT?");

            if (response.HasValue && !response.Value)
                return;
            
            parameters.TargetPlayer.SplayStack(Color.Green, SplayDirection.Right);
            PlayerActed(parameters);
        }
    }
}