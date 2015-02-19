﻿using System;
using System.Linq;
using Innovation.Models;

namespace Innovation.Actions
{
	public class Return
	{
		public static void Action(ICard card, Game game)
		{
			game.AgeDecks.First(x => x.Age == card.Age).InsertAtEnd(card);
		}
	}
}
