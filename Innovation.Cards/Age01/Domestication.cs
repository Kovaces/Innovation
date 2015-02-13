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

	    bool Action1(CardActionParameters parameters)
	    {
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Any())
			{
				var lowestAgeInHand = parameters.TargetPlayer.Hand.Min(c => c.Age);
				var lowestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

				ICard cardToMeld = parameters.TargetPlayer.PickCard(lowestCards);
				parameters.TargetPlayer.Hand.Remove(cardToMeld);
				Meld.Action(cardToMeld, parameters.TargetPlayer);
			}

			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

			return true;
	    }
    }
}