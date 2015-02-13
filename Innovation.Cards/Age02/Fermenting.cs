using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Fermenting : CardBase
    {
        public override string Name { get { return "Fermenting"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Leaf, "Draw a [2] for every two [LEAF] icons on your board.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) 
		{
			ParseParameters(parameters, 2);

			int numberOfLeafs = TargetPlayer.Tableau.GetSymbolCount(Symbol.Leaf);
			for (int i = 0; i < numberOfLeafs / 2; i++)
				TargetPlayer.Hand.Add(Draw.Action(2, Game));

			return numberOfLeafs >= 2;
		}
    }
}