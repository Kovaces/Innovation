using Innovation.Models;

namespace Innovation.Actions
{
	public class Meld
	{
		public static void Action(Card card, Player player)
		{
			player.Tableau.Stacks[card.CardColor].Cards.Add(card);
		}
	}
}
