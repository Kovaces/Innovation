using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
	public class Currency : ICard
	{
		public string Name { get { return "Currency"; } }
		public int Age { get { return 2; } }
		public Color Color { get { return Color.Green; } }
		public Symbol Top { get { return Symbol.Leaf; } }
		public Symbol Left { get { return Symbol.Crown; } }
		public Symbol Center { get { return Symbol.Blank; } }
		public Symbol Right { get { return Symbol.Crown; } }
		public IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Crown, "You may return any number of cards from your hand. If you do, draw and score a [2] for every different value card you returned.", Action1)
                };
			}
		}
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<ICard> cardsToReturn = targetPlayer.PickFromMultipleCards(targetPlayer.Hand, 0, targetPlayer.Hand.Count);
			if (cardsToReturn.Count > 0)
			{
				foreach (ICard card in cardsToReturn)
				{
					targetPlayer.Hand.Remove(card);
					Return.Action(card, game);
				}

				int differentAges = cardsToReturn.Select(x => x.Age).Distinct().Count();
				for (int i = 0; i < differentAges; i++)
					Score.Action(Draw.Action(2, game), targetPlayer);

				return true;
			}

			return false;
		}
	}
}