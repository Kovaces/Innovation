using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
    public class Domestication : CardBase
    {
        public override string Name { get { return "Domestication"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Tower; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Meld the lowest card in your hand. Draw a [1].", Action1)
                };
            }
        }

	    bool Action1(object[] parameters)
	    {
			ParseParameters(parameters, 2);

			if (TargetPlayer.Hand.Any())
			{
				var lowestAgeInHand = TargetPlayer.Hand.Min(c => c.Age);
				var lowestCards = TargetPlayer.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

				ICard cardToMeld = TargetPlayer.PickCard(lowestCards);
				TargetPlayer.Hand.Remove(cardToMeld);
				Meld.Action(cardToMeld, TargetPlayer);
			}

			TargetPlayer.Hand.Add(Draw.Action(1, Game));

			return true;
	    }
    }
}