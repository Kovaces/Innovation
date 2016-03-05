
using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Exceptions;
using Innovation.GameObjects;
using Innovation.Interfaces;

namespace Innovation.Actions
{
	public class Draw
	{
		/// <summary>
		/// The Draw action. If there is a card of the Age to draw available, take the top card of that Age.
		/// Otherwise increment the age and check again. 
		/// </summary>
		/// <param name="age">The Age of the Card to Draw</param>
		/// <param name="ageDecks">The list of age decks to draw from</param>
		public static ICard Action(int age, IEnumerable<Deck> ageDecks)
		{
			return GetCard(age, ageDecks);
		}

		private static ICard GetCard(int age, IEnumerable<Deck> ageDecks)
		{
			age = Math.Min(Math.Max(age, 1), 10);

			var ageDeck = ageDecks.First(d => d.Age.Equals(age));
			var drawnCard = ageDeck.Draw();

			if ((age != 10) && (drawnCard == null))
				drawnCard = GetCard(++age, ageDecks);

			if ((age == 10) && (drawnCard == null))
				throw new CardDrawException();

			return drawnCard;
		}
	}
}
