using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Globalization : CardBase
    {
        public override string Name => "Globalization";
        public override int Age => 10;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you return a top card with a [LEAF] on your board.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a [6]. If no player has more [LEAF] than [FACTORY] on their board, the single player with the most points wins.", Action2)
        };

        bool Action1(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

		    var selectedCard = parameters.TargetPlayer.PickCard(parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Leaf)));
			Return.Action(selectedCard, parameters.Game);

		    return false;
	    }

	    bool Action2(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

		    Score.Action(Draw.Action(6, parameters.Game), parameters.TargetPlayer);

			if (!parameters.Game.Players.Exists(p => p.Tableau.GetSymbolCount(Symbol.Leaf) > p.Tableau.GetSymbolCount(Symbol.Factory)))
				parameters.Game.TriggerEndOfGame(parameters.Game.Players.OrderByDescending(p => p.Tableau.GetScore()).ToList().First());

			return true;
	    }
    }
}