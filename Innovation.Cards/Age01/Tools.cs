using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Tools : ICard
    {
        public string Name { get { return "Tools"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return three cards from your hand. If you do, draw and meld a [3].", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a [3] from your hand. If you do, draw three [1].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) 
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			if (targetPlayer.Hand.Count >= 3)
			{
				List<ICard> cardsToReturn = targetPlayer.PickMultipleCardsFromHand(targetPlayer.Hand, 3, 3);
				foreach (ICard card in cardsToReturn)
				{
					targetPlayer.Hand.Remove(card);
					Return.Action(card, game);
				}

				Meld.Action(Draw.Action(3, game), targetPlayer);

				return true;
			}
			return false;
		}
        bool Action2(object[] parameters) 
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<ICard> ageThreeCardsInHand = targetPlayer.Hand.Where(x => x.Age == 3).ToList();
			if (ageThreeCardsInHand.Count > 0)
			{
				ICard cardToReturn = targetPlayer.PickCardFromHand(ageThreeCardsInHand);

				targetPlayer.Hand.Remove(cardToReturn);
				Return.Action(cardToReturn, game);

				targetPlayer.Hand.Add(Draw.Action(1, game));
				targetPlayer.Hand.Add(Draw.Action(1, game));
				targetPlayer.Hand.Add(Draw.Action(1, game));

				return true;
			}

			return false;
		}
    }
}