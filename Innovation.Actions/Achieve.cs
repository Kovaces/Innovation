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
		/// <param name="achievementDeck">The game's age achievement deck</param>
		/// <returns></returns>
		public static bool Action(IPlayer player, Deck achievementDeck)
		{
			if (!achievementDeck.Cards.Any())
				return false;

			var topAvailableAchievementAge = achievementDeck.Cards.First().Age;

			if (!((player.Tableau.GetHighestAge() >= topAvailableAchievementAge) && (player.Tableau.GetScore() >= (topAvailableAchievementAge*5))))
				return false;

			achievementDeck.Draw();
			return true;
		}
	}
}
