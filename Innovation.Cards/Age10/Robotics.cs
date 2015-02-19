using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Robotics : CardBase
    {
        public override string Name { get { return "Robotics"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Score your top green card. Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them.", Action1)
                };
            }
        }

	    bool Action1(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

			//Score your top green card.
		    var topGreenCard = parameters.TargetPlayer.Tableau.Stacks[Color.Green].GetTopCard();
		    if (topGreenCard != null)
		    {
			    Score.Action(topGreenCard, parameters.TargetPlayer);
				parameters.TargetPlayer.Tableau.Stacks[Color.Green].RemoveCard(topGreenCard);
		    }

			//Draw and meld a [10]
		    var drawnCard = Draw.Action(10, parameters.Game);
			if (drawnCard == null)
				return true;

			Meld.Action(drawnCard, parameters.TargetPlayer);

		    foreach (var cardAction in drawnCard.Actions)
		    {
			    if (cardAction.ActionType != ActionType.Demand)
				    cardAction.ActionHandler(new CardActionParameters {ActivePlayer = parameters.TargetPlayer, TargetPlayer = parameters.TargetPlayer, Game = parameters.Game, PlayerSymbolCounts = parameters.PlayerSymbolCounts});
		    }

		    return true;
	    }
    }
}