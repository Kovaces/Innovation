using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Philosophy : CardBase
    {
        public override string Name { get { return "Philosophy"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may splay left any one color of your cards.", Action1)
                    , new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may score a card from your hand.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) 
		{
			ParseParameters(parameters, 2);

			// pick color vs pick card?
			List<ICard> cardsToSelectFrom = TargetPlayer.Tableau.GetTopCards();
			if (cardsToSelectFrom.Count > 0)
			{
				Color chosenColor = TargetPlayer.PickMultipleCards(cardsToSelectFrom, 1, 1).First().Color;

				TargetPlayer.Tableau.Stacks[chosenColor].Splay(SplayDirection.Left);
				return true;
			}

			return false;
		}
		bool Action2(object[] parameters)
		{
			ParseParameters(parameters, 2);

			if (TargetPlayer.Hand.Count > 0)
			{
				ICard card = TargetPlayer.PickCardFromHand();
				TargetPlayer.Hand.Remove(card);
				Score.Action(card, TargetPlayer);
			}

			return false;
		}
    }
}