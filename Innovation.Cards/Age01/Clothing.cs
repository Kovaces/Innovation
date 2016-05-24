using Innovation.Actions;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
	public class Clothing : CardBase
	{
		public override string Name => "Clothing";
	    public override int Age => 1;
	    public override Color Color => Color.Green;
	    public override Symbol Top => Symbol.Blank;
	    public override Symbol Left => Symbol.Crown;
	    public override Symbol Center => Symbol.Leaf;
	    public override Symbol Right => Symbol.Leaf;

	    public override IEnumerable<ICardAction> Actions => new List<CardAction>()
	    {
	        new CardAction(ActionType.Required, Symbol.Leaf, "Meld a card from your hand of different color from any card on your board.", Action1),
	        new CardAction(ActionType.Required, Symbol.Leaf, "Draw and score a [1] for each color present on your board not present on any other player's board.", Action2)
	    };

	    void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			if (!parameters.TargetPlayer.Tableau.GetStackColors().Any())
				return;

			var topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			var cardsToMeld = parameters.TargetPlayer.Hand.Where(x => !topCardColors.Contains(x.Color)).ToList();

			if (cardsToMeld.Count == 0)
				return;

			var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = cardsToMeld, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);

			Meld.Action(selectedCard, parameters.TargetPlayer);

			PlayerActed(parameters);
		}

		void Action2(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			var targetPlayerTopCardColors = parameters.TargetPlayer.Tableau.GetStackColors();

			if (!targetPlayerTopCardColors.Any())
				return;

			var otherPlayerTopCardColors = parameters.Players.Where(p => p != parameters.TargetPlayer).SelectMany(r => r.Tableau.GetStackColors()).Distinct().ToList();

			var numberOfCardsToDraw = targetPlayerTopCardColors.Count(x => !otherPlayerTopCardColors.Contains(x));

			if (numberOfCardsToDraw == 0)
				return;

			for (var i = 0; i < numberOfCardsToDraw; i++)
			{
				Score.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);
			}

			PlayerActed(parameters);
		}
	}
}