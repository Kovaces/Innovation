using Innovation.Actions;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
	public class Domestication : CardBase
	{
		public override string Name { get { return "Domestication"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Tower; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Blank; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Meld the lowest card in your hand. Draw a [1].", Action1)
                };
			}
		}

		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			var lowestAgeInHand = parameters.TargetPlayer.Hand.Min(c => c.Age);
			var lowestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

			var cardToMeld = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = lowestCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			parameters.TargetPlayer.RemoveCardFromHand(cardToMeld);

			Meld.Action(cardToMeld, parameters.TargetPlayer);

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

			PlayerActed(parameters);
		}
	}
}