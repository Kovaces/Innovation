using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Exceptions;
using Innovation.GameObjects;
using Innovation.Interfaces;

using Innovation.Models.Other;
using Innovation.Storage;


namespace Innovation.Game
{
    public class Game
    {
        private readonly ContextStorage _storage = new ContextStorage();
        private Player.Player _winner;

        public string Id { get; set; }
        public string Name { get; set; }
        public List<Player.Player> Players { get; set; }
        public Player.Player ActivePlayer { get; set; }
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

            StartTurn?.Invoke(ActivePlayer.Id);
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
                        throw new ArgumentNullException(nameof(card), "You must select a card to take the Dogma Action");

                    try
                    {
                        _storage.RemoveFromGameStorage("AnotherPlayerTookDogmaActionKey");
                        var actionParameters = new CardActionParameters { TargetPlayer = null, ActivePlayer = ActivePlayer, AgeDecks = AgeDecks, Players = GetPlayersInPlayerOrder(Players.IndexOf(ActivePlayer)), AddToStorage = _storage.AddToGameStorage, GetFromStorage = _storage.RetrieveFromGameStorage };
                        Dogma.Action(card, actionParameters);
                    }
                    catch (CardDrawException)
                    {
                        TriggerEndOfGame();
                    }
                    catch (EndOfGameException)
                    {
                        var winner = (Player.Player)_storage.RetrieveFromGameStorage("WinnerKey");
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
                        throw new ArgumentNullException(nameof(card), "You must select a card to take the Meld Action");

                    Meld.Action(card, ActivePlayer);
                    
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), "Action not a valid player action" );
            }

            SynchGameState?.Invoke(this);

            if ((ActivePlayer.ActionsTaken == 2))
                EndTurn?.Invoke(ActivePlayer.Id);
        }

        public List<Player.Player> GetPlayersInPlayerOrder(int startingIndex)
        {
            var players = Players.GetRange(startingIndex + 1, Players.Count - startingIndex - 1);
            if (players.Count < Players.Count)
                players.AddRange(Players.GetRange(0, startingIndex + 1));

            return players;
        }

        public void TriggerEndOfGame(Player.Player winner = null)
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

