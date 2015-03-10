using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Actions.Handlers;
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
		CardActionResults Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			// pick color vs pick card?
			List<Color> colorsToSelectFrom = parameters.TargetPlayer.Tableau.GetTopCards().Select(x => x.Color).ToList();
			if (colorsToSelectFrom.Count == 0)
				return new CardActionResults(false, false);
			
			RequestQueueManager.AskToSplay(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				colorsToSelectFrom,
				SplayDirection.Left,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			Color chosenColor = parameters.Answer.Color;
			if (chosenColor == Color.None)
				return new CardActionResults(false, false);

			parameters.TargetPlayer.Tableau.Stacks[chosenColor].Splay(SplayDirection.Left);

			return new CardActionResults(true, false);
		}


		CardActionResults Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				1,
				1,
				parameters.PlayerSymbolCounts,
				Action2_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action2_Step2(CardActionParameters parameters)
		{
			ICard card = parameters.Answer.SingleCard;
			if (card == null)
				return new CardActionResults(false, false);

			parameters.TargetPlayer.Hand.Remove(card);
			Score.Action(card, parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}
    }
}