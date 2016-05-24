using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Fermenting : CardBase
    {
        public override string Name => "Fermenting";
        public override int Age => 2;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Leaf, "Draw a [2] for every two [LEAF] icons on your board.", Action1)
        };

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			int numberOfLeafs = parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Leaf);
			for (int i = 0; i < numberOfLeafs / 2; i++)
				parameters.TargetPlayer.Hand.Add(Draw.Action(2, parameters.Game));

			return numberOfLeafs >= 2;
		}
    }
}