using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Bioengineering : CardBase
    {
        public override string Name => "Bioengineering";
        public override int Age => 10;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Clock,"Transfer a top card with a [LEAF] from any other player's board to your score pile.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Clock,"If any player has fewer than three [LEAF] on their board, the single player with the most [LEAF] on their board wins.", Action2)
        };

        bool Action1(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

		    var transferCards = parameters.Game.Players.SelectMany(p => p.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Leaf)));
		    var selectedCard = parameters.TargetPlayer.PickCard(transferCards);
			parameters.Game.Players.ForEach(p => p.Tableau.Stacks[selectedCard.Color].RemoveCard(selectedCard));
			parameters.TargetPlayer.Tableau.Stacks[selectedCard.Color].AddCardToTop(selectedCard);

			return true;
	    }

	    bool Action2(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

		    if (parameters.Game.Players.Exists(p => p.Tableau.GetSymbolCount(Symbol.Leaf) < 3))
				parameters.Game.TriggerEndOfGame(parameters.Game.Players.OrderByDescending(p => p.Tableau.GetSymbolCount(Symbol.Leaf)).ToList().First());
		    
			return false;
	    }
    }
}