﻿using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Currency : CardBase
	{
        public override string Name { get { return "Currency"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Crown, "You may return any number of cards from your hand. If you do, draw and score a [2] for every different value card you returned.", Action1)
                };
			}
		}
		bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<ICard> cardsToReturn = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Hand, 0, parameters.TargetPlayer.Hand.Count).ToList();
			if (cardsToReturn.Count > 0)
			{
				foreach (ICard card in cardsToReturn)
				{
					parameters.TargetPlayer.Hand.Remove(card);
					Return.Action(card, parameters.Game);
				}

				int differentAges = cardsToReturn.Select(x => x.Age).Distinct().Count();
				for (int i = 0; i < differentAges; i++)
					Score.Action(Draw.Action(2, parameters.Game), parameters.TargetPlayer);

				return true;
			}

			return false;
		}
	}
}