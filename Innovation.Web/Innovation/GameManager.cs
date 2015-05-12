using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Innovation.Actions;
using Innovation.Cards;
using Innovation.GameObjects;
using Innovation.Interfaces;
using Innovation.Player;

namespace Innovation.Web.Innovation
{
	public class GameManager
	{
        public static void StartGame(Game.Game game)
		{
			game = InitializeGame(game);

			game.BeginTurn(game.Players.ElementAt(0).Id);
		}

        private static Game.Game InitializeGame(Game.Game game)
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
		    Parallel.ForEach(game.Players, player =>
		    {
                var card = player.Interaction.PickCards(player.Id, new PickCardParameters { CardsToPickFrom = player.Hand.ToList(), MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
		        Meld.Action(card, player);
		        player.Hand.Remove(card);
		    });
			
			WaitForFirstMeld(game);
			
			//the player order is determined by alphabetical order of the first meld
			game.Players = game.Players.OrderBy(p => p.Tableau.GetTopCards().ElementAt(0).Name).ToList();

			return game;
		}

        private static void WaitForFirstMeld(Game.Game game)
		{
		    while (!game.Players.TrueForAll(p => p.Tableau.GetTopCards().Count == 1))
		    {
		        Thread.Sleep(1000); 
		    } 
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

        private static Deck CreateAgeAchievementDeck(Game.Game game)
		{
			var cards = new List<ICard>();

			for (var i = 1; i < 10; i++)
			{
				cards.Add(Draw.Action(i, game.AgeDecks));
			}

			return new Deck(cards, -1);
		}

        private static void DealStartingCards(Game.Game game)
		{
			game.Players.ForEach(p => p.AddCardToHand(Draw.Action(1, game.AgeDecks)));
			game.Players.ForEach(p => p.AddCardToHand(Draw.Action(1, game.AgeDecks)));
		}
	}
}