﻿using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Monotheism : CardBase
    {
        public override string Name => "Monotheism";
        public override int Age => 2;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Tower;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!", Action1)
            ,new CardAction(ActionType.Required, Symbol.Tower, "Draw and tuck a [1].", Action2)
        };

        bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> activePlayerTopColors = parameters.ActivePlayer.Tableau.GetStackColors();
			List<ICard> possibleTransferCards = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => !activePlayerTopColors.Contains(x.Color)).ToList();

			if (possibleTransferCards.Count == 0)
				return false;
			
			ICard cardToTransfer = parameters.TargetPlayer.PickCard(possibleTransferCards);
			parameters.TargetPlayer.Tableau.Stacks[cardToTransfer.Color].RemoveCard(cardToTransfer);
			parameters.ActivePlayer.Tableau.ScorePile.Add(cardToTransfer);

			Tuck.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);

			return true;
		}
        bool Action2(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			Tuck.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);

			return true;
		}
    }
}