using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Pottery : ICard
    {
        public string Name { get { return "Pottery"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Leaf, "You may return up to three cards from your hand. If you returned any cards, draw and score a card of value equal to the number of cards you returned.", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Leaf, "Draw a [1].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) 
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<ICard> selectedCards = targetPlayer.PickFromMultipleCards(targetPlayer.Hand, 0, 3);
			foreach (ICard card in selectedCards)
			{
				targetPlayer.Hand.Remove(card);
				Return.Action(card, game);
			}
			if (selectedCards.Count > 0)
			{
				Score.Action(Draw.Action(selectedCards.Count, game), targetPlayer);
				return true;
			}

			return false;
		}
		bool Action2(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			targetPlayer.Hand.Add(Draw.Action(1, game));

			return true;
		}
    }
}