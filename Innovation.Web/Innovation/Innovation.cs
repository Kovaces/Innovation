using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Innovation.Cards;
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
			/*
		 * Create game
		 *		Create Age Decks
		 *		Create Special Achievement Decks
		 *		Create Age Achievement Deck
		 *		Determine # of players
		 *		Deal starting Age 1 cards
		 *		First Meld
		 */

			var game = new Game();
			var cardList = CardList.GetCardList().ToList();

			var age1 = new Deck(cardList.Where(c => c.Age == 1), 1);
			var age2 = new Deck(cardList.Where(c => c.Age == 2), 2);
			var age3 = new Deck(cardList.Where(c => c.Age == 3), 3);
			var age4 = new Deck(cardList.Where(c => c.Age == 4), 4);
			var age5 = new Deck(cardList.Where(c => c.Age == 5), 5);
			var age6 = new Deck(cardList.Where(c => c.Age == 6), 6);
			var age7 = new Deck(cardList.Where(c => c.Age == 7), 7);
			var age8 = new Deck(cardList.Where(c => c.Age == 8), 8);
			var age9 = new Deck(cardList.Where(c => c.Age == 9), 9);
			var age10 = new Deck(cardList.Where(c => c.Age == 10), 10);

			game.AgeDecks = new List<Deck> { age1, age2, age3, age4, age5, age6, age7, age8, age9, age10 };
			game.AgeDecks.ForEach(d => d.Shuffle());
			
			var achievementDeck = new List<ICard>();
			game.AgeDecks.ForEach(d => achievementDeck.Add(d.Cards[0]));
			game.AgeDecks.ForEach(d => d.Cards.RemoveAt(0));
			game.AgeAchievementDeck = new Deck(achievementDeck, -1);


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
