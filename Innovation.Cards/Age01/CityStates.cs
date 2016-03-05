using Innovation.Actions;

using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;

using Innovation.Player;

namespace Innovation.Cards
{
    public class CityStates : CardBase
    {
		public override string Name { get { return "City States"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Demand, Symbol.Crown, "I demand you transfer a top card with a [TOWER] from your board to my board if you have at least four [TOWER] on your board! If you do, draw a [1]!", Action1)
                };
            }
        }
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Tower) < 4)
				return;
			
			var topCardsWithTowers = new List<ICard>();

			foreach (var stack in parameters.TargetPlayer.Tableau.Stacks.Values)
			{
				var card = stack.GetTopCard();

				if ((card != null) && (card.HasSymbol(Symbol.Tower)))
					topCardsWithTowers.Add(card);
			}

			if (!topCardsWithTowers.Any())
				return;

			var cardToMove = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = topCardsWithTowers, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			// remove from TargetPlayer's board
			parameters.TargetPlayer.Tableau.Stacks[cardToMove.Color].RemoveCard(cardToMove);

			// add to ActivePlayer's board - doesn't say meld, so just add it
			parameters.ActivePlayer.Tableau.Stacks[cardToMove.Color].AddCardToTop(cardToMove);

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));
		}
    }
}