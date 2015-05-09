using System.Linq;
using Innovation.Models;
using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models.Enums;
using Innovation.Models.Other;
using Innovation.Players;

namespace Innovation
{
	public class Game
	{
		private readonly ContextStorage _storage = new ContextStorage();
		private Player _winner;

		public string Id { get; set; }
		public string Name { get; set; }
		public List<Player> Players { get; set; }
		public Player ActivePlayer { get; set; }
		public List<Deck> AgeDecks { get; set; }
		public Deck AgeAchievementDeck { get; set; }
		public bool GameEnded { get; set; }

        public Action<Game> SynchGameState { get; set; }
        public Action<string> StartTurn { get; set; }
        public Action<string> EndTurn { get; set; }

		public void BeginTurn(string playerId)
		{
			ActivePlayer = Players.First(p => p.Id.Equals(playerId, StringComparison.InvariantCultureIgnoreCase));
			ActivePlayer.ActionsTaken = 0;
		    
            if (StartTurn != null)
		        StartTurn(ActivePlayer.Id);
		}

		public void TakeAction(ActionEnum action, ICard card = null)
		{
			if (ActivePlayer.ActionsTaken == 2)
				throw new InvalidOperationException("Player has already take the maxium actions for their turn");

			ActivePlayer.ActionsTaken++;

			switch (action)
			{
				case ActionEnum.Achieve:
					if (Achieve.Action(ActivePlayer, AgeAchievementDeck))
						ActivePlayer.Achievements++;
					break;

				case ActionEnum.Dogma:
					if (card == null)
						throw new ArgumentNullException("card", "You must select a card to take the Dogma Action");

					try
					{
						_storage.RemoveFromGameStorage(ContextStorage.AnotherPlayerTookDogmaActionKey);
						Dogma.Action(card, GetPlayersInPlayerOrder(Players.IndexOf(ActivePlayer)), ActivePlayer, AgeDecks, _storage.AddToGameStorage, _storage.RetrieveFromGameStorage);
					}
					catch (CardDrawException)
					{
						TriggerEndOfGame();
					}
					catch (EndOfGameException)
					{
						var winner = (Player)_storage.RetrieveFromGameStorage(ContextStorage.WinnerKey);
						TriggerEndOfGame(winner);
					}
					
					break;

				case ActionEnum.Draw:
					try
					{
						var cardDrawn = Draw.Action(ActivePlayer.Tableau.GetHighestAge(), AgeDecks);
						ActivePlayer.Hand.Add(cardDrawn);
					}
					catch (CardDrawException)
					{
						TriggerEndOfGame();
					}
					break;

				case ActionEnum.Meld:
					if (card == null)
						throw new ArgumentNullException("card", "You must select a card to take the Meld Action");

					Meld.Action(card, ActivePlayer);
					
					break;

				default:
					throw new ArgumentOutOfRangeException("action", "Action not a valid player action" );
			}

            if (SynchGameState != null)
			    SynchGameState(this);

			if ((ActivePlayer.ActionsTaken == 2) && (EndTurn != null))
                EndTurn(ActivePlayer.Id);
		}

		public List<Player> GetPlayersInPlayerOrder(int startingIndex)
		{
			var players = Players.GetRange(startingIndex + 1, Players.Count - startingIndex - 1);
			if (players.Count < Players.Count)
				players.AddRange(Players.GetRange(0, startingIndex + 1));

			return players;
		}

		public void TriggerEndOfGame(Player winner = null)
		{
			GameEnded = true;

			if (_winner != null)
				_winner = winner;

			//TODO: calculate winner by score
			_winner = Players.ElementAt(0);

			//GameOverHandler(Name, _winner.Id);
		}

		
		
	}
}

