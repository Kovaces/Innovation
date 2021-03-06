﻿using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class CanalBuilding : CardBase
    {
        public override string Name => "Canal Building";
        public override int Age => 2;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Crown, "You may exchange all the highest cards in your hand with all the highest cards in your score pile.", Action1)
        };

        bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.AskQuestion(this.Actions.ElementAt(0).ActionText))
				return false;
			
			int maxAgeInHand = parameters.TargetPlayer.Hand.Any() ? parameters.TargetPlayer.Hand.Max(x => x.Age) : 0;
			var cardsInHandToTransfer = parameters.TargetPlayer.Hand.Where(x => x.Age == maxAgeInHand).ToList();

			int maxAgeInPile = parameters.TargetPlayer.Tableau.ScorePile.Any() ? parameters.TargetPlayer.Tableau.ScorePile.Max(x => x.Age) : 0;
			var cardsInPileToTransfer = parameters.TargetPlayer.Tableau.ScorePile.Where(x => x.Age == maxAgeInPile).ToList();

			if (!cardsInHandToTransfer.Any() && !cardsInPileToTransfer.Any())
				return false; //no game state change regardless of player opting in

			foreach (ICard card in cardsInHandToTransfer)
			{
				parameters.TargetPlayer.Hand.Remove(card);
				parameters.TargetPlayer.Tableau.ScorePile.Add(card);
			}

			foreach (ICard card in cardsInPileToTransfer)
			{
				parameters.TargetPlayer.Tableau.ScorePile.Remove(card);
				parameters.TargetPlayer.Hand.Add(card);
			}
			
			return true;
		}
    }
}