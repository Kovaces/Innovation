﻿using Innovation.Models;

namespace Innovation.Actions
{
	public class Dogma
	{
		public static void Action(Card card, Player player, Game game)
		{
			card.Actions.ForEach(a => a.ActionHandler(new object[] { player, game }));
		}
	}
}
