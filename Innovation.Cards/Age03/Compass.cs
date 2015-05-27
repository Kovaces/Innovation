using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
	public class Compass : CardBase
	{
		public override string Name { get { return "Compass"; } }
		public override int Age { get { return 3; } }
		public override Color Color { get { return Color.Green; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Crown; } }
		public override Symbol Right { get { return Symbol.Leaf; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
					new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-green card with a [LEAF] from your board to my board, and then you transfer a top card without a [LEAF] from my board to your board.", Action1)
				};
			}
		}

		private void Action1(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var topCardsWithLeaf = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => (c.Color != Color.Green) && c.HasSymbol(Symbol.Leaf)).ToList();

			if (topCardsWithLeaf.Any())
			{
				var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters {CardsToPickFrom = topCardsWithLeaf, MinimumCardsToPick = 1, MaximumCardsToPick = 1}).First();
				parameters.TargetPlayer.RemoveCardFromStack(selectedCard);
				parameters.ActivePlayer.AddCardToStack(selectedCard);
			}

			var topCardsWithoutLeaf = parameters.ActivePlayer.Tableau.GetTopCards().Where(c => !c.HasSymbol(Symbol.Leaf)).ToList();

			if (topCardsWithoutLeaf.Any())
			{
				var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = topCardsWithoutLeaf, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
				parameters.ActivePlayer.RemoveCardFromStack(selectedCard);
				parameters.TargetPlayer.AddCardToStack(selectedCard);
			}
		}
	}
}