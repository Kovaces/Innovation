﻿using System;
using Innovation.Models;
using Innovation.Models.Interfaces;

namespace Innovation.Actions
{
	public class Score
	{
		public static void Action(ICard card, IPlayer player)
		{
			if (card == null)
				throw new NullReferenceException("Card cannot be null");

			player.Tableau.ScorePile.Add(card);
		}
	}
}