using Innovation.Cards;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Innovation.Game;
using Innovation.Interfaces;
using Innovation.Player;
using Newtonsoft.Json;

namespace Innovation.Web.Innovation
{
	public class Innovation
	{
		private readonly static Lazy<Innovation> _instance = new Lazy<Innovation>(() => new Innovation(GlobalHost.ConnectionManager.GetHubContext<InnovationHub>().Clients));

		private readonly ConcurrentDictionary<string, Game.Game> _games = new ConcurrentDictionary<string, Game.Game>();
		private readonly ConcurrentDictionary<string, Player.Player> _players = new ConcurrentDictionary<string, Player.Player>();

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

		private Player.Player GetPlayerById(string id)
		{
			Player.Player tempPlayer = null;

			_players.TryGetValue(id, out tempPlayer);

			if (tempPlayer == null)
				throw new ArgumentNullException("Invalid Player.  [" + id + "]");

			return tempPlayer;
		}
		private Game.Game GetGameById(string id)
		{
			Game.Game tempGame = null;

			_games.TryGetValue(id, out tempGame);

			if (tempGame == null)
				throw new ArgumentNullException("Invalid Game.  [" + id + "]");

			return tempGame;
		}
		private Game.Game GetGameByPlayerId(string playerId)
		{
			Player.Player player;
			player = GetPlayerById(playerId);

			var game = _games.Values.FirstOrDefault(g => g.Players.Contains(player));

			if (game == null)
				throw new InvalidOperationException("Player : [" + player.Name + "] not part of any games.");

			return game;
		}

		private void ValidateResponseParameters(string gameId, string playerId, out Game.Game game, out Player.Player player)
		{
			game = GetGameById(gameId);
			player = GetPlayerById(playerId);
			if (!game.Players.Contains(player))
				throw new ArgumentNullException("Player [" + playerId + "] not in game [" + gameId + "]");
		}

		//User
		public void AddUser(string userId)
		{
			var interaction = new PlayerInteraction
			{
				AskQuestionHandler = new AskQuestion { Handler = AskQuestion },
				PickCardsHandler = new PickCards { Handler = PickCards },
				PickColorHandler = new PickColor { Handler = PickColor },
				PickPlayerHandler = new PickPlayer { Handler = PickPlayer },
				RevealCardHandler = new RevealCard { Handler = RevealCard }
			};

			var player = new Player.Player
			{
				Id = userId,
				UpdateClientHandler = UpdateClient,
				Interaction = interaction,
			};

			_players.TryAdd(player.Id, player);
		}
		public void RemoveUser(string userId)
		{
			Player.Player player;
			_players.TryRemove(userId, out player);
		}

		//Game
		public void CreateGame(string gameName, string[] playerIds)
		{
			
			
			var game = new Game.Game()
			{
				Name = gameName,
				Id = Guid.NewGuid().ToString(),
				Players = playerIds.Select(id => GetPlayerById(id)).ToList(),
				SynchGameState = SyncGameState,
				StartTurn = PlayerStartTurn,
				EndTurn = PlayerEndTurn
			};

			_games.TryAdd(game.Id, game);

			Clients.Group(game.Name).setGameId(game.Id);

			Task.Factory.StartNew(() => GameManager.StartGame(game)).Wait();

			SyncGameState(game);

			foreach (var p in game.Players)
				p.UpdateClientHandler = UpdateClient;
		}
		public void GameOver(string gameName, string winner)
		{
			Clients.Group(gameName).gameOverNotification(GetPlayerById(winner).Name);

			Game.Game game;
			_games.TryRemove(gameName, out game);
		}

		public void SyncGameState(string gameId)
		{
			var game = GetGameById(gameId);
			SyncGameState(game);
		}
		public void SyncGameState(Game.Game game)
		{
			var thing = new
			{
				ActivePlayer = (game.ActivePlayer == null ? null : game.ActivePlayer.Id),
				AgeAchievementDeck = (game.AgeAchievementDeck == null ? null : new
				{
					Age = game.AgeAchievementDeck.Age,
					Cards = game.AgeAchievementDeck.Cards.Select(x => x.Id)
				}),
				Id = game.Id,
				Name = game.Name,
				AgeDecks = (game.AgeDecks == null ? null : game.AgeDecks.Select(x=> new {
					Age = x.Age,
					CardCount = x.Cards.Count
				})),
				Players = game.Players.Select(x => new
				{
					ActionsTaken = x.ActionsTaken,
					Id = x.Id,
					Name = x.Name,
					Hand = (x.Hand == null ? null : x.Hand.Select(h => h.Id)),
					Tableau = new
					{
						NumberOfAchievements = x.Tableau.NumberOfAchievements,
						ScorePile = x.Tableau.ScorePile.Select(p => p.Id),
						Stacks = x.Tableau.Stacks.Keys.Select(s => new
						{
							Color = s,
							SplayedDirection = x.Tableau.Stacks[s].SplayedDirection,
							Cards = x.Tableau.Stacks[s].Cards.Select(c => c.Id),
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
		public void PlayerEndTurn(string playerId)
		{
			Clients.User(playerId).startTurn();
		}

		internal void PlayerPickAction(string gameId, string playerId, string selectedAction)
		{
			Game.Game game;
			Player.Player player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			ActionEnum action;
			if (!Enum.TryParse(selectedAction, true, out action))
				throw new InvalidEnumArgumentException();

			game.TakeAction(action);
		}

		public void PickCards(string playerId, PickCardParameters pickCardParameters)
		{
			Clients.Client(playerId).broadcastMessage("pickr", "pick a card!");
			Clients.Client(playerId).pickCard(
				JsonConvert.SerializeObject(pickCardParameters.CardsToPickFrom.Select(x => x.Id).ToList())
				, pickCardParameters.MinimumCardsToPick
				, pickCardParameters.MaximumCardsToPick
			);
		}
		internal void PickCardsResponse(string gameId, string playerId, string[] cardIds)
		{
			Game.Game game;
			Player.Player player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			player.Interaction.PickCardsHandler.Response(CardList.GetCardList().Where(c => cardIds.ToList().Contains(c.Id)));
		}

		public void AskQuestion(string playerId, string question)
		{
			Clients.User(playerId).askQuestion(question);
		}
		internal void AskQuestionResponse(string gameId, string playerId, bool response)
		{
			Game.Game game;
			Player.Player player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			player.Interaction.AskQuestionHandler.Response(response);
		}

		public void PickPlayer(string playerId, IEnumerable<IPlayer> playerList)
		{
			Clients.User(playerId).pickPlayers(
				Newtonsoft.Json.JsonConvert.SerializeObject(playerList.Select(x => x.Id).ToList())
				, 1
				, 1
			);
		}
		internal void PickPlayerResponse(string gameId, string playerId, string selectedPlayerId)
		{
			Game.Game game;
			Player.Player player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			player.Interaction.PickPlayerHandler.Response(_players.First(p => p.Key.Equals(selectedPlayerId)).Value);
		}
		
		public void PickColor(string playerId, IEnumerable<Color> colors)
		{
			;
		}
		internal void PickColorResponse(string gameId, string playerId, string selectedColor)
		{
			Game.Game game;
			Player.Player player;
			ValidateResponseParameters(gameId, playerId, out game, out player);

			Color color;
			if (!Enum.TryParse(selectedColor, true, out color))
				throw new InvalidEnumArgumentException();

			player.Interaction.PickColorHandler.Response(color);
		}

		public void RevealCard(string playerId, ICard card)
		{
			Game.Game game = GetGameByPlayerId(playerId);
			Clients.Group(game.Name).revealCard(playerId, JsonConvert.SerializeObject(card));
			
			Player.Player player = GetPlayerById(playerId);
			player.Interaction.RevealCardHandler.Response(true);
		}

		public void UpdateClient(string playerId)
		{
			var game = GetGameByPlayerId(playerId);
			SyncGameState(game);
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

//public void PickPlayers(string playerId, IEnumerable<Player> playerList, int minimumNumberToSelect, int maximumNumberToSelect)
//{
//    Clients.User(playerId).pickPlayers(
//        Newtonsoft.Json.JsonConvert.SerializeObject(playerList.Select(x => x.Id).ToList())
//        , minimumNumberToSelect
//        , maximumNumberToSelect
//    );
//}
//internal void PickPlayersResponse(string gameId, string playerId, string[] selectedPlayers)
//{
//    Game game;
//    Player player;
//    ValidateResponseParameters(gameId, playerId, out game, out player);

//    player.Interaction.PickPlayerHandler.Response(_players.First(p => selectedPlayers.First().Equals(p.Key)).Value);
//}
//public void AskToSplay(string playerId, IEnumerable<Color> colors, SplayDirection splayDirection)
//{
//    Clients.User(playerId).askToSplay(
//        Newtonsoft.Json.JsonConvert.SerializeObject(colors.ToList()),
//        Newtonsoft.Json.JsonConvert.SerializeObject(splayDirection)
//    );
//}
//internal void PickSplayResponse(string gameId, string playerId, string selectedColor)
//{
//    Game game;
//    Player player;
//    ValidateResponseParameters(gameId, playerId, out game, out player);

//    RequestQueueManager.ReceiveSplayResponse(game, player, selectedColor);
//}