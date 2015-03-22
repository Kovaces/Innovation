using System.Web.Helpers;
using Innovation.Actions.Handlers;
using Innovation.Cards;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Innovation.Web.Innovation
{
	public class Innovation
	{
		private readonly static Lazy<Innovation> _instance = new Lazy<Innovation>(() => new Innovation(GlobalHost.ConnectionManager.GetHubContext<InnovationHub>().Clients));

		private readonly ConcurrentDictionary<string, Game> _games = new ConcurrentDictionary<string, Game>();
		private readonly ConcurrentDictionary<string, IPlayer> _players = new ConcurrentDictionary<string, IPlayer>();

		private IHubConnectionContext<dynamic> Clients { get; set; }

		private Innovation(IHubConnectionContext<dynamic> clients)
		{
			Clients = clients;

			_games.Clear();
			_players.Clear();
		}

		public static Innovation Instance
		{
			get { return _instance.Value; }
		}

		private IPlayer GetPlayerById(string id)
		{
			IPlayer tempPlayer = null;

			_players.TryGetValue(id, out tempPlayer);

			if (tempPlayer == null)
				throw new ArgumentNullException("Invalid Player.  [" + id + "]");

			return tempPlayer;
		}
		private Game GetGameById(string id)
		{
			Game tempGame = null;

			_games.TryGetValue(id, out tempGame);

			if (tempGame == null)
				throw new ArgumentNullException("Invalid Game.  [" + id + "]");

			return tempGame;
		}
		private Game GetGameByPlayerId(string playerId)
		{
			IPlayer player;
			player = GetPlayerById(playerId);

			var game = _games.Values.FirstOrDefault(g => g.Players.Contains(player));

			if (game == null)
				throw new InvalidOperationException("Player : [" + player.Name + "] not part of any games.");

			return game;
		}

		private void ValidateResponseParameters(string gameId, string playerId, out Game game, out IPlayer player)
		{
			game = GetGameById(gameId);
			player = GetPlayerById(playerId);
			if (!game.Players.Contains(player))
				throw new ArgumentNullException("Player [" + playerId + "] not in game [" + gameId + "]");
		}

		//User
		public void AddUser(string userId)
		{
			var player = new Player
			{
				Id = userId,
				PickMultipleCardsHandler = PickCards,
				AskQuestionHandler = AskQuestion,
				PickPlayersHandler = PickPlayers,
				StartTurnHandler = PlayerStartTurn,
				RevealCardHandler = RevealCard,
				UpdateClientHandler = UpdateClient,
			};

			_players.TryAdd(player.Id, player);
		}
		public void RemoveUser(string userId)
		{
			IPlayer player;
			_players.TryRemove(userId, out player);
		}

		//Game
		public void CreateGame(string gameName, string[] playerIds)
		{
			var game = new Game()
			{
				Name = gameName,
				Id = new Guid().ToString(),
				Players = playerIds.Select(id => GetPlayerById(id)).ToList(),
				GameOverHandler = GameOver,
				SyncGameStateHandler = SyncGameState,
			};

			_games.TryAdd(gameName, game);

			Clients.Group(game.Name).setGameId(game.Id);

			Task.Factory.StartNew(() => GameManager.StartGame(game));
			SyncGameState(game);
		}
		public void GameOver(string gameName, string winner)
		{
			Clients.Group(gameName).gameOverNotification(GetPlayerById(winner).Name);

			Game game;
			_games.TryRemove(gameName, out game);
		}

		public void SyncGameState(string gameId)
		{
			var game = GetGameById(gameId);
			SyncGameState(game);
		}
		public void SyncGameState(Game game)
		{
			var thing = new
			{
				ActivePlayer = (game.ActivePlayer == null ? null : game.ActivePlayer.Id),
				AgeAchievementDeck = (game.AgeAchievementDeck == null ? null : new
				{
					Age = game.AgeAchievementDeck.Age,
					Cards = game.AgeAchievementDeck.Cards.Select(x => new
					{
						Id = x.Id
					})
				}),
				Id = game.Id,
				Name = game.Name,
				Players = game.Players.Select(x => new
				{
					ActionsTaken = x.ActionsTaken,
					Id = x.Id,
					Name = x.Name,
					Tableau = new
					{
						NumberOfAchievements = x.Tableau.NumberOfAchievements,
						ScorePile = x.Tableau.ScorePile.Select(p => new
						{
							Id = p.Id
						}),
						Stacks = x.Tableau.Stacks.Keys.Select(s => new
						{
							Color = s,
							SplayedDirection = x.Tableau.Stacks[s].SplayedDirection,
							Cards = x.Tableau.Stacks[s].Cards.Select(c => new
							{
								Id = c.Id
							}),
						}),
					}
				})
			};

			Clients.Group(game.Name).syncGameState(JsonConvert.SerializeObject(thing));
		}

		//Player
		public void PlayerStartTurn(string playerId)
		{
			Clients.User(playerId).startTurn();
		}
		internal void PickAction(string gameId, string playerId, string selectedAction)
		{
			Game game;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			ActionQueueManager.PerformAction(game, player, selectedAction);
		}

		public void PickCards(string playerId, IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			Clients.User(playerId).pickCard(
				JsonConvert.SerializeObject(cardsToSelectFrom.Select(x => x.Id).ToList())
				, minimumNumberToSelect
				, maximumNumberToSelect
			);
		}
		internal void PickCardsResponse(string gameId, string playerId, string[] cardIds)
		{
			Game game;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			RequestQueueManager.ReceiveCardResponse(game, player, cardIds.ToList());
		}

		public void AskQuestion(string playerId, string question)
		{
			Clients.User(playerId).askQuestion(question);
		}
		internal void AskQuestionResponse(string gameId, string playerId, bool response)
		{
			Game game;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			RequestQueueManager.ReceiveBooleanResponse(game, player, response);
		}

		public void PickPlayers(string playerId, IEnumerable<IPlayer> playerList, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			Clients.User(playerId).pickPlayers(
				Newtonsoft.Json.JsonConvert.SerializeObject(playerList.Select(x => x.Id).ToList())
				, minimumNumberToSelect
				, maximumNumberToSelect
			);
		}
		internal void PickPlayersResponse(string gameId, string playerId, string[] selectedPlayers)
		{
			Game game;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			RequestQueueManager.ReceivePlayerResponse(game, player, selectedPlayers.ToList());
		}

		public void AskToSplay(string playerId, IEnumerable<Color> colors, SplayDirection splayDirection)
		{
			Clients.User(playerId).askToSplay(
				Newtonsoft.Json.JsonConvert.SerializeObject(colors.ToList()),
				Newtonsoft.Json.JsonConvert.SerializeObject(splayDirection)
			);
		}
		internal void PickSplayResponse(string gameId, string playerId, string selectedColor)
		{
			Game game;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			RequestQueueManager.ReceiveSplayResponse(game, player, selectedColor);
		}

		public void RevealCard(string playerId, ICard card)
		{
			Clients.User(playerId).revealCard(JsonConvert.SerializeObject(card));
		}

		public void UpdateClient(string playerId)
		{
			var game = GetGameByPlayerId(playerId);
			Clients.Group(game.Name).syncGameState(JsonConvert.SerializeObject(game));
		}

		//data
		internal object GetCardList()
		{
			return CardList.GetCardList()
							.Select(c => new
							{
								Id = c.Id,
								Name = c.Name,
								Color = c.Color,
								Age = c.Age,
								Top = c.Top,
								Left = c.Left,
								Center = c.Center,
								Right = c.Right,
								Actions = c.Actions.Select(a => new
								{
									Symbol = a.Symbol,
									ActionType = a.ActionType,
									ActionText = a.ActionText
								}),
								Image = string.Empty
							}).ToList();
		}
		internal object GetPlayerList()
		{
			return _players.Values
							.Select(c => new
							{
								Id = c.Id,
								Name = c.Name
							}).ToList();
		}
		internal void SetNameToClient(string clientId, string name)
		{
			_players[clientId].Name = name;
		}
	}
}