using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;



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
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Leaf, "Draw a [2] for every two [LEAF] icons on your board.", Action1)
                };
            }
        }
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			int numberOfLeafs = parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Leaf);

			for (int i = 0; i < (numberOfLeafs / 2); i++)
				parameters.TargetPlayer.AddCardToHand(Draw.Action(2, parameters.AgeDecks));

			if (numberOfLeafs >= 2)
				PlayerActed(parameters);
		}
    }
}