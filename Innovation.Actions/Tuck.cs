using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions
{
	public class Tuck
	{
		public static void Action(ICard card, IPlayer player)
		{
			player.Tableau.Stacks[card.Color].AddCardToBottom(card);
		}
	}
}
