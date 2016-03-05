using Innovation.Actions;

using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;

using Innovation.Player;

namespace Innovation.Cards
{
	public class Agriculture : CardBase
	{
		public override string Name { get { return "Agriculture"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Leaf; } }
		public override Symbol Center { get { return Symbol.Leaf; } }
		public override Symbol Right { get { return Symbol.Leaf; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>() 
				{
                    new CardAction(ActionType.Optional, Symbol.Leaf, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action)
                };
			}
		}

		private void Action(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.");

			if (!answer.HasValue || !answer.Value)
				return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			
			Return.Action(selectedCard, parameters.AgeDecks);

			Score.Action(Draw.Action(selectedCard.Age + 1, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
	}
}