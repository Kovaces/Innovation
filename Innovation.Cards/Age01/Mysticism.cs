using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
	public class Mysticism : ICard
	{
		public string Name { get { return "Mysticism"; } }
		public int Age { get { return 1; } }
		public Color Color { get { return Color.Purple; } }
		public Symbol Top { get { return Symbol.Blank; } }
		public Symbol Left { get { return Symbol.Tower; } }
		public Symbol Center { get { return Symbol.Tower; } }
		public Symbol Right { get { return Symbol.Tower; } }
		public IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Draw and reveal a [1]. If it is the same color as any card on your board, meld it and draw a [1].", Action1)
                };
			}
		}
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			ICard card = Draw.Action(1, game);
			targetPlayer.RevealCard(card);

			List<Color> stackColors = targetPlayer.Tableau.GetStackColors();
			if (stackColors.Contains(card.Color))
			{
				Meld.Action(card, targetPlayer);
				targetPlayer.Hand.Add(Draw.Action(1, game));

				return true;
			}
			else
				targetPlayer.Hand.Add(card);

			return false;
		}
	}
}