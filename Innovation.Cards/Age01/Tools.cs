using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
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

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count < 3)
				return false;

			List<ICard> cardsToReturn = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Hand, 3, 3).ToList();
			
			if (cardsToReturn.Count == 0)
				return false;

			foreach (ICard card in cardsToReturn)
			{
				parameters.TargetPlayer.Hand.Remove(card);
				Return.Action(card, parameters.Game);
			}

			Meld.Action(Draw.Action(3, parameters.Game), parameters.TargetPlayer);

			return true;
		}

        bool Action2(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			List<ICard> ageThreeCardsInHand = parameters.TargetPlayer.Hand.Where(x => x.Age == 3).ToList();

			if (ageThreeCardsInHand.Count == 0)
				return false;

			ICard cardToReturn = parameters.TargetPlayer.PickCard(ageThreeCardsInHand);

	        if (cardToReturn == null)
		        return false;

			parameters.TargetPlayer.Hand.Remove(cardToReturn);
			Return.Action(cardToReturn, parameters.Game);

			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));
			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));
			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

			return true;
		}
    }
}