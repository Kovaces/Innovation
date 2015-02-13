using Innovation.Models;
using System.Linq;
using Innovation.Models.Interfaces;

namespace Innovation.Actions
{
	public class Achieve
	{
		/// <summary>
		/// If a Player has a top card in their Tableau of the same Age or greate than the Age of the top Achievement card
		/// AND a score of at least 5 times the Age of the top Achievement card then they can achieve.
		/// If these conditions are true the Achieve action will remove the top Achievement card and return true otherwise false.
		/// </summary>
		/// <param name="player">Player performing the action</param>
		/// <param name="game">Game the action is being performed in</param>
		/// <returns></returns>
		public static bool Action(IPlayer player, Game game)
		{
			if (!game.AgeAchievementDeck.Cards.Any())
				return false;

			var topAvailableAchievementAge = game.AgeAchievementDeck.Cards.First().Age;

			if (!((player.Tableau.GetHighestAge() >= topAvailableAchievementAge) && (player.Tableau.GetScore() >= (topAvailableAchievementAge*5))))
				return false;
			
			game.AgeAchievementDeck.Draw();
			return true;
		}
	}
}
