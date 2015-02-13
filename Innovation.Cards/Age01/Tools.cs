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
        bool Action1(object[] parameters) 
		{
			ParseParameters(parameters, 2);

			if (TargetPlayer.Hand.Count < 3)
				return false;

			List<ICard> cardsToReturn = TargetPlayer.PickMultipleCards(TargetPlayer.Hand, 3, 3).ToList();
			if (cardsToReturn.Count == 0)
				return false;

			foreach (ICard card in cardsToReturn)
			{
				TargetPlayer.Hand.Remove(card);
				Return.Action(card, Game);
			}

			Meld.Action(Draw.Action(3, Game), TargetPlayer);

			return true;
		}

        bool Action2(object[] parameters) 
		{
			ParseParameters(parameters, 2);

			List<ICard> ageThreeCardsInHand = TargetPlayer.Hand.Where(x => x.Age == 3).ToList();

			if (ageThreeCardsInHand.Count == 0)
				return false;

			ICard cardToReturn = TargetPlayer.PickCard(ageThreeCardsInHand);

	        if (cardToReturn == null)
		        return false;

			TargetPlayer.Hand.Remove(cardToReturn);
			Return.Action(cardToReturn, Game);

			TargetPlayer.Hand.Add(Draw.Action(1, Game));
			TargetPlayer.Hand.Add(Draw.Action(1, Game));
			TargetPlayer.Hand.Add(Draw.Action(1, Game));

			return true;
		}
    }
}