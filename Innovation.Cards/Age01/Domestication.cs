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
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Tower,"Meld the lowest card in your hand. Draw a [1].", Action1)
                };
            }
        }

	    bool Action1(object[] parameters)
	    {
			if (parameters.Length < 2)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Player and Game");

			var player = parameters[0] as Player;
			if (player == null)
				throw new NullReferenceException("Player cannot be null");

			var game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");

			if (player.Hand.Any())
			{
				var lowestAgeInHand = player.Hand.Min(c => c.Age);
				var lowestCards = player.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

				Meld.Action((lowestCards.Count() == 1) ? lowestCards[0] : player.PickCardFromHand(lowestCards), player);
			}

			player.Hand.Add(Draw.Action(1, game));

			return true;
	    }
    }
}