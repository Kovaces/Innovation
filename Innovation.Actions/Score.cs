using System;
using Innovation.Models;

namespace Innovation.Actions
{
	public class Score
	{
		public static void Action(ICard card, Player player)
		{
			if (card == null)
				throw new NullReferenceException("Card cannot be null");

			player.Tableau.ScorePile.Add(card);
		}
	}
}