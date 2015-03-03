using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Innovation.Models;
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

		public void AddUser(string userId)
		{
			var player = new Player { Id = userId, PickCardHandler = PickCard, PickMultipleCardsHandler = PickMultipleCards, AskQuestionHandler = AskQuestion, PickPlayerHandler = PickPlayer };
			_players.TryAdd(player.Id, player);
		}

		public void RemoveUser(string userId)
		{
			IPlayer player;
			_players.TryRemove(userId, out player);
		}

		public void CreateGame(string gameName, string[] playerIds)
		{
			
			throw new NotImplementedException();
		}

		public void PickCard(string playerId, IEnumerable<ICard> cardsToSelectFrom)
		{
			var transactionId = Guid.NewGuid();
			var cardIds = cardsToSelectFrom.Select(c => c.Name);

			_responsesPending.TryAdd(transactionId, playerId);

			Clients.User(playerId).pickCard(transactionId, Newtonsoft.Json.JsonConvert.SerializeObject(cardIds));
		}
		internal void PickCardResponse(Guid transactionId, Guid gameId, string cardName)
		{
			throw new NotImplementedException();
		}

		public void PickMultipleCards(string playerId, IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			throw new NotImplementedException();
		}
		internal void PickMultipleCardsResponse(Guid transactionId, Guid gameId, string[] cardNames)
		{
			throw new NotImplementedException();
		}

		public void AskQuestion(string playerId, string question)
		{
			throw new NotImplementedException();
		}
		internal void AskQuestionResponse(Guid transactionId, Guid gameId, bool response)
		{
			throw new NotImplementedException();
		}

		public void PickPlayer(string playerId, List<IPlayer> playerList)
		{
			throw new NotImplementedException();
		}
		internal void PickPlayerResponse(Guid transactionId, Guid gameId, string selectedPlayer)
		{
			throw new NotImplementedException();
		}
	}
}
