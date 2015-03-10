using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions.Handlers;
namespace Innovation.Cards
{
    public class Tools : CardBase
    {
        public override string Name { get { return "Tools"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return three cards from your hand. If you do, draw and meld a [3].", Action1)
                    ,new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return a [3] from your hand. If you do, draw three [1].", Action2)
                };
            }
        }

        CardActionResults Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count < 3)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				3,
				3,
				parameters.PlayerSymbolCounts,
				Action2_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action2_Step2(CardActionParameters parameters)
		{
			List<ICard> cardsToReturn = parameters.Answer.MultipleCards;
		
			if (cardsToReturn.Count == 0)
				return new CardActionResults(false, false);

			foreach (ICard card in cardsToReturn)
			{
				parameters.TargetPlayer.Hand.Remove(card);
				Return.Action(card, parameters.Game);
			}

			var drawnCard = Draw.Action(3, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			Meld.Action(drawnCard, parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}

        CardActionResults Action2(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			List<ICard> ageThreeCardsInHand = parameters.TargetPlayer.Hand.Where(x => x.Age == 3).ToList();

			if (ageThreeCardsInHand.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			ICard cardToReturn = parameters.Answer.SingleCard;
			if (cardToReturn == null)
				return new CardActionResults(false, false);

			parameters.TargetPlayer.Hand.Remove(cardToReturn);
			Return.Action(cardToReturn, parameters.Game);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);
			parameters.TargetPlayer.Hand.Add(drawnCard);

			drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);
			parameters.TargetPlayer.Hand.Add(drawnCard);

			drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);
			parameters.TargetPlayer.Hand.Add(drawnCard);

			return new CardActionResults(true, false);
		}
    }
}