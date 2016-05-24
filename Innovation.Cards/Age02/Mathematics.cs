using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Mathematics : CardBase
    {
        public override string Name => "Mathematics";
        public override int Age => 2;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned.", Action1)
        };

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			ICard card = parameters.TargetPlayer.PickCardFromHand();
	        
			if (card == null)
		        return false;

			parameters.TargetPlayer.Hand.Remove(card);
			
			Return.Action(card, parameters.Game);
			
			Meld.Action(Draw.Action(card.Age + 1, parameters.Game), parameters.TargetPlayer);
			
			return true;
		}
    }
}