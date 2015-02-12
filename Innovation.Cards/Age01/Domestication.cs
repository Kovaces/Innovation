using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
    public class Domestication : ICard
    {
        public string Name { get { return "Domestication"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
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
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			if (targetPlayer.Hand.Any())
			{
				var lowestAgeInHand = targetPlayer.Hand.Min(c => c.Age);
				var lowestCards = targetPlayer.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

				ICard cardToMeld = targetPlayer.PickFromMultipleCards(lowestCards, 1, 1).First();
				targetPlayer.Hand.Remove(cardToMeld);
				Meld.Action(cardToMeld, targetPlayer);
			}

			targetPlayer.Hand.Add(Draw.Action(1, game));

			return true;
	    }
    }
}