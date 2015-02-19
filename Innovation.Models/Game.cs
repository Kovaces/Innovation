using Innovation.Models;
using System;
using System.Collections.Generic;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public class Game
	{
		public Game()
		{
			GameEnded = false;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<IPlayer> Players { get; set; }
		public List<Deck> AgeDecks { get; set; }
		public Deck AgeAchievementDeck { get; set; }

		
		private readonly Dictionary<string, object> _propertyBag = new Dictionary<string,object>();
		public object GetPropertyBagValue(string key)
		{
			if (_propertyBag.ContainsKey(key))
				return _propertyBag[key];

			return null;
		}
		public void StashPropertyBagValue(string key, object value)
		{
			if (_propertyBag.ContainsKey(key))
				_propertyBag[key] = value;
			else
				_propertyBag.Add(key, value);
		}
		public void ClearPropertyBag()
		{
			_propertyBag.Clear();
		}


		public bool GameEnded { get; private set; }
		private IPlayer _winner;
		public void TriggerEndOfGame(IPlayer winner = null)
		{
			GameEnded = true;
			
			if (_winner != null)
				_winner = winner;
		}


		public List<IPlayer> GetPlayersInPlayerOrder(int startingIndex)
		{
			var players = Players.GetRange(startingIndex + 1, Players.Count - startingIndex - 1);
			if (players.Count < Players.Count)
				players.AddRange(Players.GetRange(0, startingIndex + 1));

			return players;
		}
	}
}
