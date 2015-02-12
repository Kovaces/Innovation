using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Philosophy : ICard
    {
        public string Name { get { return "Philosophy"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Lightbulb; } }
        public IEnumerable<CardAction> Actions
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
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			// pick color vs pick card?
			List<ICard> cardsToSelectFrom = targetPlayer.Tableau.GetTopCards();
			if (cardsToSelectFrom.Count > 0)
			{
				Color chosenColor = targetPlayer.PickFromMultipleCards(cardsToSelectFrom, 1, 1).First().Color;

				targetPlayer.Tableau.Stacks[chosenColor].Splay(SplayDirection.Left);
				return true;
			}

			return false;
		}
		bool Action2(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			if (targetPlayer.Hand.Count > 0)
			{
				ICard card = targetPlayer.PickCardFromHand();
				targetPlayer.Hand.Remove(card);
				Score.Action(card, targetPlayer);
			}

			return false;
		}
    }
}