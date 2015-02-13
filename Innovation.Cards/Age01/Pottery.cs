using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Pottery : CardBase
    {
        public override string Name { get { return "Pottery"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<CardAction> Actions
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

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			var selectedCards = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Hand, 0, 3).ToList();

			if (selectedCards.Count == 0)
				return false;

			foreach (ICard card in selectedCards)
			{
				parameters.TargetPlayer.Hand.Remove(card);
				Return.Action(card, parameters.Game);
			}
			
			Score.Action(Draw.Action(selectedCards.Count, parameters.Game), parameters.TargetPlayer);
			
			return true;
		}

		bool Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

			return true;
		}
    }
}