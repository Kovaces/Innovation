using Innovation.Actions;
using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Cards
{
	public class Mysticism : CardBase
	{
		public override string Name => "Mysticism";
	    public override int Age => 1;
	    public override Color Color => Color.Purple;
	    public override Symbol Top => Symbol.Blank;
	    public override Symbol Left => Symbol.Tower;
	    public override Symbol Center => Symbol.Tower;
	    public override Symbol Right => Symbol.Tower;

	    public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
	        new CardAction(ActionType.Required,Symbol.Tower,"Draw and reveal a [1]. If it is the same color as any card on your board, meld it and draw a [1].", Action1)
	    };

	    void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			var card = DrawAndReveal(parameters, 1);

			if (parameters.TargetPlayer.Tableau.GetStackColors().Contains(card.Color))
			{
				Meld.Action(card, parameters.TargetPlayer);
				
				parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));
			}
			else
				parameters.TargetPlayer.AddCardToHand(card);

			PlayerActed(parameters);
		}
	}
}