using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Innovation.Actions;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
	public class Alchemy : CardBase
	{
		public override string Name => "Alchemy";
	    public override int Age => 3;
	    public override Color Color => Color.Blue;
	    public override Symbol Top => Symbol.Blank;
	    public override Symbol Left => Symbol.Leaf;
	    public override Symbol Center => Symbol.Tower;
	    public override Symbol Right => Symbol.Tower;

	    public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
	        new CardAction(ActionType.Required,Symbol.Tower,"Draw and reveal a [4] for every three [TOWER] on your board. If any of the drawn cards are red, return the cards drawn and return all cards in your hand. Otherwise, keep them.", Action1)
	        ,new CardAction(ActionType.Required,Symbol.Tower,"Meld a card from your hand, then score a card from your hand.", Action2)
	    };

	    void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			//Draw and reveal a [4] for every three [TOWER] on your board.
			var cardsDrawn = new List<ICard>();
			var numberOfCardsToDraw = parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Tower) / 3;

			if (numberOfCardsToDraw == 0)
				return;

			PlayerActed(parameters);

			for (var i = 0; i < numberOfCardsToDraw; i++)
			{
				var card = DrawAndReveal(parameters, 4);
				parameters.TargetPlayer.AddCardToHand(card);
				cardsDrawn.Add(card);
			}

			//If any of the drawn cards are red, return the cards drawn and return all cards in your hand. Otherwise, keep them.
			if (cardsDrawn.Any(c => c.Color == Color.Red))
			{
				parameters.TargetPlayer.Hand.ForEach(c => Return.Action(c, parameters.AgeDecks));
				parameters.TargetPlayer.Hand.ForEach(c => parameters.TargetPlayer.RemoveCardFromHand(c));
			}
		}

		void Action2(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			var cardChosen = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			Meld.Action(cardChosen, parameters.TargetPlayer);
			parameters.TargetPlayer.RemoveCardFromHand(cardChosen);

			PlayerActed(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			cardChosen = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			Score.Action(cardChosen, parameters.TargetPlayer);
			parameters.TargetPlayer.RemoveCardFromHand(cardChosen);
		}
	}
}