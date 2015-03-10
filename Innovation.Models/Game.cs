using System.Linq;
using Innovation.Models;
using System;
using System.Threading;
using System.Collections.Generic;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public delegate void GameOver(string gameName, string winner);

	public class Game
	{
		public Game()
		{
			GameEnded = false;
		}

		public bool IsWaiting { get; set; }
		public Thread Thread {get;set;}
		public bool IsRunning { get; set; }
		public bool IsProcessing { get; set; }
		public IPlayer ActivePlayer { get; set; }


		public string Id { get; set; }
		public string Name { get; set; }
		public List<IPlayer> Players { get; set; }
		public List<Deck> AgeDecks { get; set; }
		public Deck AgeAchievementDeck { get; set; }

		public RequestQueue RequestQueue { get; set; }
		public ActionQueue ActionQueue { get; set; }

		private readonly Dictionary<string, object> _propertyBag = new Dictionary<string, object>();
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

		public GameOver GameOverHandler { get; set; }
		public bool GameEnded { get; private set; }
		private IPlayer _winner;
		public void TriggerEndOfGame(IPlayer winner = null)
		{
			GameEnded = true;

			if (_winner != null)
				_winner = winner;

			//TODO: calculate winner by score
			_winner = Players.ElementAt(0);
			
			GameOverHandler(Name, _winner.Id);
		}

		public List<IPlayer> GetPlayersInPlayerOrder(int startingIndex)
		{
			var players = Players.GetRange(startingIndex + 1, Players.Count - startingIndex - 1);
			if (players.Count < Players.Count)
				players.AddRange(Players.GetRange(0, startingIndex + 1));

			return players;
		}

		public void RevealCard(ICard card)
		{
			foreach (var player in Players)
			{
				player.RevealCard(card);
			}
		}

		public IPlayer GetNextPlayer()
		{
			return GetPlayersInPlayerOrder(Players.IndexOf(ActivePlayer)).ElementAt(0);
		}
	}
}
