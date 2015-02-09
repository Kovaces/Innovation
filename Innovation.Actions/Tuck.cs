using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Actions
{
	public class Tuck
	{
		public static void Action(ICard card, Player player)
		{
			player.Tableau.Stacks[card.Color].AddCardToBottom(card);
		}
	}
}
