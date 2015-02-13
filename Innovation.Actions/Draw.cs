using Innovation;
using Innovation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innovation.Actions
{
	public class Draw
	{
		/// <summary>
		/// The Draw action. If there is a card of the Age to draw available, take the top card of that Age.
		/// Otherwise increment the age and check again. 
		/// If a player attempts to draw a card from the Age 10 deck and there are no cards, the game ends
		/// </summary>
		/// <param name="age">The Age of the Card to Draw</param>
		/// <param name="game">The Game to perform the Action in</param>
		public static ICard Action(int age, Game game)
		{
			return GetCard(age, game);
		}

		private static ICard GetCard(int age, Game game)
		{
			age = Math.Min(Math.Max(age, 1), 10);

			var ageDeck = game.AgeDecks.First(d => d.Age.Equals(age));
			var drawnCard = ageDeck.Draw();

			if (drawnCard == null)
			{
				if (age != 10)
					drawnCard = GetCard(++age, game);
				else
				{
					game.TriggerEndOfGame();
				}
			}

			return drawnCard;
		}
	}
}
