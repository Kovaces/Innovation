using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Perspective : CardBase
    {
        public override string Name => "Perspective";
        public override int Age => 4;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, score a card from your hand for every two [BULB] on your board.", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (parameters.TargetPlayer.Hand.Count <= 0)
                return;

            //You may return a card from your hand
            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, 
                                                                            new PickCardParameters
                                                                            {
                                                                                CardsToPickFrom = parameters.TargetPlayer.Hand,
                                                                                MaximumCardsToPick = 1,
                                                                                MinimumCardsToPick = 0
                                                                            }).FirstOrDefault();

            if (selectedCard == null)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
            Return.Action(selectedCard, parameters.AgeDecks);

            //If you do, score a card from your hand for every two [BULB] on your board.
            var numOfLightBulbs = parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Lightbulb);

            if (numOfLightBulbs < 2)
                return;

            var cardsToScore = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                            new PickCardParameters
                                                                            {
                                                                                CardsToPickFrom = parameters.TargetPlayer.Hand,
                                                                                MaximumCardsToPick = (numOfLightBulbs / 2),
                                                                                MinimumCardsToPick = (numOfLightBulbs / 2)
                                                                            }).ToList();

            foreach (var card in cardsToScore)
            {
                parameters.TargetPlayer.RemoveCardFromHand(card);
                Score.Action(card, parameters.TargetPlayer);
            }
        }
    }
}