﻿using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class Agriculture : ICard
	{
		public string Name { get { return "Agriculture"; } }
		public int Age { get { return 1; } }
		public Color Color { get { return Color.Yellow; } }
		public Symbol Top { get { return Symbol.Blank; } }
		public Symbol Left { get { return Symbol.Leaf; } }
		public Symbol Center { get { return Symbol.Leaf; } }
		public Symbol Right { get { return Symbol.Leaf; } }
		public IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action1)
                };
			}
		}
		bool Action1(object[] parameters)
		{
			if (parameters.Length < 2)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Player and Game");

			var player = parameters[0] as Player;
			if (player == null)
				throw new NullReferenceException("Player cannot be null");

			var game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");

			if (player.Hand.Any())
			{
				ICard selectedCard = player.PickCardFromHand();
				player.Hand.Remove(selectedCard);
				Return.Action(selectedCard, game);

				int ageToDraw = selectedCard.Age + 1;
				var cardToScore = Draw.Action(ageToDraw, game);
				Score.Action(cardToScore, player);

				return true;
			}
			else
				return false;
		}
	}
}