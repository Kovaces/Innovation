using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class Agriculture : CardBase
	{
		public override string Name => "Agriculture";
	    public override int Age => 1;
	    public override Color Color => Color.Yellow;
	    public override Symbol Top => Symbol.Blank;
	    public override Symbol Left => Symbol.Leaf;
	    public override Symbol Center => Symbol.Leaf;
	    public override Symbol Right => Symbol.Leaf;

	    public override IEnumerable<CardAction> Actions => new List<CardAction>() 
	    {
	        new CardAction(ActionType.Optional, Symbol.Leaf, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action1)
	    };

	    private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			ICard selectedCard = parameters.TargetPlayer.PickCardFromHand();
			
			if (selectedCard == null)
				return false;

			parameters.TargetPlayer.Hand.Remove(selectedCard);
			Return.Action(selectedCard, parameters.Game);

			int ageToDraw = selectedCard.Age + 1;
			var cardToScore = Draw.Action(ageToDraw, parameters.Game);
			Score.Action(cardToScore, parameters.TargetPlayer);

			return true;
		}
	}
}