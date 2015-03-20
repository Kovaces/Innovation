using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Cards;
using Innovation.Models;
using Innovation.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Innovation.Web.Innovation
{
	public class GameManager
	{
		public static void StartGame(Game game)
		{
			game = InitializeGame(game);

			RequestQueueManager.StartTurn(game, game.Players.ElementAt(0));

			while (!game.GameEnded)
			{
				WaitForResponses(game);

				if (!game.ActionQueue.IsEmpty)
				{
					game.ActionQueue.PopAction();
				}

				if (game.ActivePlayer.ActionsTaken == 2)
					RequestQueueManager.StartTurn(game, game.GetNextPlayer());

				if (!game.RequestQueue.HasPendingRequests() && game.ActionQueue.IsEmpty)
					Thread.Sleep(1000);
			}
		}

		private static Game InitializeGame(Game game)
		{
			game.AgeDecks = CreateAgeDecks();
			game.AgeDecks.ForEach(d => d.Shuffle());

			//TODO: Create Special Achievement Decks
			//Monument	- Claim immediately if you tuck 6 cards or score 6 cards during a turn
			//Empire	- Claim immediately if you have 3 or more icons of all 6 types
			//World		- Claim immediately if you have 12 or more clocks on your board
			//Wonder	- Claim immediately if you have 5 colors on your board, and each is splayed either up or right
			//Universe	- Claim immediately if you have 5 top cards, and each is value 8 or higher

			game.AgeAchievementDeck = CreateAgeAchievementDeck(game);

			DealStartingCards(game);

			//FirstMeld
			game.Players.ForEach(player =>
				game.ActionQueue.AddAction(new QueuedAction()
				{
					Game = game,
					ActivePlayer = player,
					TargetPlayer = player,
					Type = QueuedActionType.PickCard,
					Parameters = new ActionParameters()
					{
						Cards = player.Hand.ToList(),
						MinSelections = 1,
						MaxSelections = 1,
						ResponseHandler = RequestQueueManager.MeldResponse
					}
				}));

			game.Players.ForEach(player => ActionQueueManager.PopNextAction(game));

			WaitForResponses(game);
			
			//the player order is determined by alphabetical order of the first meld
			game.Players = game.Players.OrderBy(p => p.Tableau.GetTopCards().ElementAt(0).Name).ToList();

			game.ClearPropertyBag();

			return game;
		}

		private static async void WaitForResponses(Game game)
		{
			await Task.Factory.StartNew(() => { while (game.RequestQueue.HasPendingRequests()) { Thread.Sleep(1000); } });
		}

		private static List<Deck> CreateAgeDecks()
		{
			var ageDecks = new List<Deck>();

			var cards = CardList.GetCardList().ToList();

			for (var i = 1; i < 11; i++)
			{
				ageDecks.Add(new Deck(cards.Where(c => c.Age == i).ToList(), i));
			}

			return ageDecks;
		}

		private static Deck CreateAgeAchievementDeck(Game game)
		{
			var cards = new List<ICard>();

			for (var i = 1; i < 10; i++)
			{
				cards.Add(Draw.Action(i, game));
			}

			return new Deck(cards, -1);
		}

		private static void DealStartingCards(Game game)
		{
			game.Players.ForEach(p => p.AddCardToHand(Draw.Action(1, game)));
			game.Players.ForEach(p => p.AddCardToHand(Draw.Action(1, game)));
		}
	}
}