using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Measurement : CardBase
    {
        public override string Name => "Measurement";
        public override int Age => 5;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, splay any one color of your cards right, and draw a card of value equal to the number of cards of that color on your board.", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Hand.Any())
                return;

            //You may return a card from your hand. 
            var cardToReturn = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters
                                                                                                        {
                                                                                                            CardsToPickFrom = parameters.TargetPlayer.Hand,
                                                                                                            MinimumCardsToPick = 0,
                                                                                                            MaximumCardsToPick = 1
                                                                                                        }).FirstOrDefault();

            if (cardToReturn == null)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.RemoveCardFromHand(cardToReturn);
            Return.Action(cardToReturn, parameters.AgeDecks);

            //If you do, splay any one color of your cards right
            var colorToSplay = parameters.TargetPlayer.Interaction.PickColor(parameters.TargetPlayer.Id, parameters.TargetPlayer.Tableau.GetStackColors());
            parameters.TargetPlayer.SplayStack(colorToSplay, SplayDirection.Right);

            //and draw a card of value equal to the number of cards of that color on your board
            var numberOfCards = parameters.TargetPlayer.Tableau.Stacks[colorToSplay].Cards.Count;
            parameters.TargetPlayer.AddCardToHand(Draw.Action(numberOfCards, parameters.AgeDecks));
        }
    }
}