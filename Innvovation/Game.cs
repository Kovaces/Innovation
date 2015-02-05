using Innovation.Models;
using System;
using System.Collections.Generic;

namespace Innovation
{
	public class Game
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<Player> Players { get; set; }
		public List<Deck> AgeDecks { get; set; }
		public Deck AgeAchievementDeck { get; set; }

		public void TriggerEndOfGame()
		{
			throw new NotImplementedException();
		}
	}
}
