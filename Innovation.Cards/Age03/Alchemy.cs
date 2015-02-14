using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
    public class Alchemy : CardBase
    {
        public override string Name { get { return "Alchemy"; } }
        public override int Age { get { return 3; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Tower; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Tower,"Draw and reveal a [4] for every three [TOWER] on your board. If any of the drawn cards are red, return the cards drawn and return all cards in your hand. Otherwise, keep them.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Tower,"Meld a card from your hand, then score a card from your hand.", Action2)
                };
            }
        }

	    bool Action1(CardActionParameters parameters)
	    {
			ValidateParameters(parameters);

			//Draw and reveal a [4] for every three [TOWER] on your board.
		    var cardsDrawn = new List<ICard>();
		    var numberOfCardsToDraw = parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Tower) / 3;

		    if (numberOfCardsToDraw == 0)
			    return false;

		    for (int i = 0; i < numberOfCardsToDraw; i++)
		    {
			    ICard card = Draw.Action(4, parameters.Game);
                parameters.TargetPlayer.RevealCard(card); 
                parameters.TargetPlayer.Hand.Add(card);
				cardsDrawn.Add(card);
		    }
			
			//If any of the drawn cards are red, return the cards drawn and return all cards in your hand. Otherwise, keep them.
		    if (cardsDrawn.Any(c => c.Color == Color.Red))
		    {
			    parameters.TargetPlayer.Hand.ForEach(c => Return.Action(c, parameters.Game));
			    parameters.TargetPlayer.Hand.RemoveRange(0, parameters.TargetPlayer.Hand.Count());
		    }

		    return true;
	    }

	    bool Action2(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return false;

		    var cardChosen = parameters.TargetPlayer.PickCardFromHand();
			Meld.Action(cardChosen, parameters.TargetPlayer);
		    parameters.TargetPlayer.Hand.Remove(cardChosen);

			cardChosen = parameters.TargetPlayer.PickCardFromHand();
			Score.Action(cardChosen, parameters.TargetPlayer);
			parameters.TargetPlayer.Hand.Remove(cardChosen);

			return true;
	    }
    }
}