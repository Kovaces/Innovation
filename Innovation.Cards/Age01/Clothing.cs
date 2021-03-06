﻿using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
	public class Clothing : CardBase
	{
		public override string Name => "Clothing";
		public override int Age => 1;
		public override Color Color => Color.Green;
		public override Symbol Top => Symbol.Blank;
		public override Symbol Left => Symbol.Crown;
		public override Symbol Center => Symbol.Leaf;
		public override Symbol Right => Symbol.Leaf;

		public override IEnumerable<CardAction> Actions => new List<CardAction>()
		{
			new CardAction(ActionType.Required, Symbol.Leaf, "Meld a card from your hand of different color from any card on your board.", Action1),
			new CardAction(ActionType.Required, Symbol.Leaf, "Draw and score a [1] for each color present on your board not present on any other player's board.", Action2)
		};

		private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			List<ICard> cardsToMeld = parameters.TargetPlayer.Hand.Where(x => !topCardColors.Contains(x.Color)).ToList();

			if (cardsToMeld.Count == 0)
				return false;
			
			ICard card = parameters.TargetPlayer.PickCard(cardsToMeld);

			parameters.TargetPlayer.Hand.Remove(card);
			Meld.Action(card, parameters.TargetPlayer);

			return true;	
		}

		bool Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> targetPlayerTopCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			List<Color> otherPlayerTopCardColors = parameters.Game.Players.Where(p => p != parameters.TargetPlayer).SelectMany(r => r.Tableau.GetStackColors()).Distinct().ToList();

			var numberOfCardsToDraw = targetPlayerTopCardColors.Count(x => !otherPlayerTopCardColors.Contains(x));
			for (var i = 0; i < numberOfCardsToDraw; i++)
				Score.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);

			return (numberOfCardsToDraw > 0);
		}
	}
}