using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Sailing : CardBase
    {
        public override string Name { get { return "Sailing"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Crown, "Draw and meld a [1].", Action1)
                };
            }
        }
		bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 2);

			Meld.Action(Draw.Action(1, Game), TargetPlayer);

			return true;
		}
    }
}