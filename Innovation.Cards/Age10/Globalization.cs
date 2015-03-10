using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Globalization : CardBase
    {
        public override string Name { get { return "Globalization"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you return a top card with a [LEAF] on your board.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a [6]. If no player has more [LEAF] than [FACTORY] on their board, the single player with the most points wins.", Action2)
                };
            }
        }

	    CardActionResults Action1(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

			var topCardsWithLeaves = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Leaf)).ToList();
			if (topCardsWithLeaves.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				topCardsWithLeaves,
				1,
				1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			var selectedCard = parameters.Answer.SingleCard;
			if (selectedCard == null)
				throw new ArgumentNullException("Must choose a card.");

			Return.Action(selectedCard, parameters.Game);

			return new CardActionResults(true, false);
		}

     CardActionResults Action2(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

		    Score.Action(Draw.Action(6, parameters.Game), parameters.TargetPlayer);

			if (!parameters.Game.Players.Exists(p => p.Tableau.GetSymbolCount(Symbol.Leaf) > p.Tableau.GetSymbolCount(Symbol.Factory)))
				parameters.Game.TriggerEndOfGame(parameters.Game.Players.OrderByDescending(p => p.Tableau.GetScore()).ToList().First());

			return new CardActionResults(true, false);
		}
    }
}