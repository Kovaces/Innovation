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
        public override string Name => "Philosophy";
        public override int Age => 2;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may splay left any one color of your cards.", Action1)
            , new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may score a card from your hand.", Action2)
        };

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			// pick color vs pick card?
			List<ICard> cardsToSelectFrom = parameters.TargetPlayer.Tableau.GetTopCards();
			if (cardsToSelectFrom.Count > 0)
			{
				Color chosenColor = parameters.TargetPlayer.PickMultipleCards(cardsToSelectFrom, 1, 1).First().Color;

				parameters.TargetPlayer.Tableau.Stacks[chosenColor].Splay(SplayDirection.Left);
				return true;
			}

			return false;
		}
		bool Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count > 0)
			{
				ICard card = parameters.TargetPlayer.PickCardFromHand();
				parameters.TargetPlayer.Hand.Remove(card);
				Score.Action(card, parameters.TargetPlayer);
			}

			return false;
		}
    }
}