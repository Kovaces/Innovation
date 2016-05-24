using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Robotics : CardBase
    {
        public override string Name => "Robotics";
        public override int Age => 10;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Score your top green card. Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them.", Action1)
        };

        void Action1(ICardActionParameters parameters)
	    {
			

			ValidateParameters(parameters);

			//Score your top green card.
		    var topGreenCard = parameters.TargetPlayer.Tableau.Stacks[Color.Green].GetTopCard();
		    if (topGreenCard != null)
		    {
			    Score.Action(topGreenCard, parameters.TargetPlayer);
				parameters.TargetPlayer.RemoveCardFromStack(topGreenCard);
		    }

			//Draw and meld a [10]
		    var drawnCard = Draw.Action(10, parameters.AgeDecks);
			Meld.Action(drawnCard, parameters.TargetPlayer);

		    var newParameters = new CardActionParameters
		    {
			    ActivePlayer = parameters.TargetPlayer,
			    TargetPlayer = parameters.TargetPlayer,
			    AgeDecks = parameters.AgeDecks,
			    AddToStorage = parameters.AddToStorage,
			    GetFromStorage = parameters.GetFromStorage,
			    Players = parameters.Players,
		    };

		    foreach (var cardAction in drawnCard.Actions)
		    {
			    if (cardAction.ActionType != ActionType.Demand)
					cardAction.ActionHandler(newParameters);
		    }

			PlayerActed(parameters);
		}
    }
}