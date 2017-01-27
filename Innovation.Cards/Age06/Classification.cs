using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Classification : CardBase
    {
        public override string Name => "Classification";
        public override int Age => 6;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Lightbulb,"Reveal the color of a card from your hand. Take into your hand all cards of that color from all other player's hands. Then, meld all cards of that color from your hand.", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Hand.Any())
                return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                new PickCardParameters
                                                                                {
                                                                                    CardsToPickFrom = parameters.TargetPlayer.Hand,
                                                                                    MinimumCardsToPick = 1,
                                                                                    MaximumCardsToPick = 1
                                                                                }).First();

            parameters.TargetPlayer.Interaction.RevealCard(parameters.TargetPlayer.Id, selectedCard);

            foreach (var player in parameters.Players)
            {
                if (player.Id == parameters.TargetPlayer.Id)
                    continue;

                var cardsToTransfer = player.Hand.Where(c => c.Color == selectedCard.Color).ToList();

                foreach (var card in cardsToTransfer)
                {
                    player.RemoveCardFromHand(card);
                    parameters.TargetPlayer.AddCardToHand(card);
                }
            }

            var cardsToMeld = parameters.TargetPlayer.Hand.Where(c => c.Color == selectedCard.Color).ToList();
            var meldCardsInThisOrder = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                        new PickCardParameters
                                                                                        {
                                                                                            CardsToPickFrom = cardsToMeld,
                                                                                            MinimumCardsToPick = cardsToMeld.Count,
                                                                                            MaximumCardsToPick = cardsToMeld.Count
                                                                                        }).ToList();

            foreach (var card in meldCardsInThisOrder)
            {
                parameters.TargetPlayer.RemoveCardFromHand(card);
                Meld.Action(card, parameters.TargetPlayer);
            }

            PlayerActed(parameters);
        }
    }
}