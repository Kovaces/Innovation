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
				throw new ArgumentNullException("Invalid Game.  " + id + "]");
			
			return tempGame;
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
				StartTurnHandler = PlayerStartTurn
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
				GameOverHandler = GameOver
			};

			_games.TryAdd(gameName, game);
			
			Task.Factory.StartNew(() => GameManager.StartGame(game));
		}
		public void GameOver(string gameName, string winner)
		{
			Clients.Group(gameName).gameOverNotification(GetPlayerById(winner).Name);
			
			Game game;
			_games.TryRemove(gameName, out game);
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
				Newtonsoft.Json.JsonConvert.SerializeObject(cardsToSelectFrom.Select(x => x.Id).ToList())
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

		//data
		internal object GetCardList()
		{
			return CardList.GetCardList()
							.Select(c => new
							{
								CardId = c.Id,
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
	}
}