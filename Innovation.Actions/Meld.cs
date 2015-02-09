using Innovation.Models;

namespace Innovation.Actions
{
	public class Meld
	{
		public static void Action(ICard card, Player player)
		{
			player.Tableau.Stacks[card.Color].AddCardToTop(card);
		}
	}
}
