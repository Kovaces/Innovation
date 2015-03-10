using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using Innovation.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Innovation
{
	public class Innovation
	{
		private readonly static Lazy<Innovation> _instance = new Lazy<Innovation>(() => new Innovation(GlobalHost.ConnectionManager.GetHubContext<InnovationHub>().Clients));

		private readonly ConcurrentDictionary<string, Game> _games = new ConcurrentDictionary<string, Game>();
		private readonly ConcurrentDictionary<string, IPlayer> _players = new ConcurrentDictionary<string, IPlayer>();
		private readonly ConcurrentDictionary<Guid, string> _responsesPending = new ConcurrentDictionary<Guid, string>();

		private IHubConnectionContext<dynamic> Clients { get; set; }

		private Innovation(IHubConnectionContext<dynamic> clients)
		{
			Clients = clients;

			_games.Clear();
		}

		public static Innovation Instance
		{
			get { return _instance.Value; }
		}

		public IPlayer GetPlayerById(string id)
		{
			IPlayer tempPlayer;
			if (_players.TryGetValue(id, out tempPlayer)
					&& tempPlayer != null)
				return tempPlayer;
			else
				throw new ArgumentNullException("Invalid Player.  [" + id + "]");
		}
		public Game GetGameById(string id)
		{
			Game tempGame = null;
			if (_games.TryGetValue(id, out tempGame)
					&& tempGame != null)
				return tempGame;
			else
				throw new ArgumentNullException("Invalid Game.  " + id + "]");
		}



		public void AddUser(string userId)
		{
			var player = new Player { Id = userId, PickMultipleCardsHandler = PickCards, AskQuestionHandler = AskQuestion, PickPlayersHandler = PickPlayers };
			_players.TryAdd(player.Id, player);
		}

		public void RemoveUser(string userId)
		{
			IPlayer player;
			_players.TryRemove(userId, out player);
		}

		public void CreateGame(string gameName, string[] playerIds)
		{
			Game game = new Game()
			{
				Name = gameName,
				Id = new Guid().ToString()
			};
			List<IPlayer> playersToGame = new List<IPlayer>();
			foreach (string id in playerIds)
			{
				playersToGame.Add(GetPlayerById(id));
			}
			game.Players = playersToGame;
		}


		public void ValidateResponseParameters(string gameId, string playerId, out Game game, out IPlayer player)
		{
			game = GetGameById(gameId);
			player = GetPlayerById(playerId);
			if (!game.Players.Contains(player))
				throw new ArgumentNullException("Player [" + playerId + "] not in game [" + gameId + "]");
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
			Game game = null;
			if (_games.TryGetValue(gameId, out game)
					|| game == null)
				throw new ArgumentNullException("Invalid Game.  " + gameId);

			IPlayer player = GetPlayerById(playerId);

			if (!game.Players.Contains(player))
				throw new ArgumentNullException("Player [" + playerId + "] not in game [" + gameId + "]");

			List<string> cardList = cardIds.ToList();

			RequestQueueManager.ReceiveCardResponse(game, player, cardList);
		}



		public void AskQuestion(string playerId, string question)
		{
			Clients.User(playerId).askQuestion(
				question
			);
		}
		internal void AskQuestionResponse(string gameId, string playerId, bool response)
		{
			Game game = null;
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
			Game game = GetGameById(gameId);
			IPlayer player = GetPlayerById(playerId);
			if (!game.Players.Contains(player))
				throw new ArgumentNullException("Player [" + playerId + "] not in game [" + gameId + "]");

			RequestQueueManager.ReceivePlayerResponse(game, player, selectedPlayers.ToList());
		}


		public void AskToSplay(string playerId, IEnumerable<Color> colors, SplayDirection splayDirection)
		{
			Clients.User(playerId).askToSplay(
				Newtonsoft.Json.JsonConvert.SerializeObject(colors.ToList()),
				Newtonsoft.Json.JsonConvert.SerializeObject(splayDirection)
			);
		}
		public void PickSplayResponse(string gameId, string playerId, string selectedColor)
		{
			Game game = null;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			RequestQueueManager.ReceiveSplayResponse(game, player, selectedColor);
		}





		public void PickAction(string playerId)
		{
			Clients.User(playerId).pickAction();
		}
		public void PickActionResponse(string gameId, string playerId, string selectedAction)
		{
			Game game = null;
			IPlayer player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			RequestQueueManager.ReceiveActionResponse(game, player, selectedAction);
		}
	}
}