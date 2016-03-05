using System;
using Innovation.Interfaces;


namespace Innovation.Actions
{
	public class Score
	{
		public static void Action(ICard card, IPlayer player)
		{
			if (card == null)
				throw new NullReferenceException("Card cannot be null");

			player.AddCardToScorePile(card);
		}
	}
}