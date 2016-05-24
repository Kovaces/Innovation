using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
    public class Tools : CardBase
    {
        public override string Name => "Tools";
        public override int Age => 1;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return three cards from your hand. If you do, draw and meld a [3].", Action1)
            ,new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return a [3] from your hand. If you do, draw three [1].", Action2)
        };

        void Action1(ICardActionParameters parameters) 
		{
			

			ValidateParameters(parameters);

	        if (parameters.TargetPlayer.Hand.Count < 3)
		        return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return three cards from your hand. If you do, draw and meld a [3].");

			if (!answer.HasValue || !answer.Value)
				return;

			var selectedCards = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 3, MaximumCardsToPick = 3 }).ToList();

			foreach (var card in selectedCards)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				Return.Action(card, parameters.AgeDecks);
			}

			Meld.Action(Draw.Action(3, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}


        void Action2(ICardActionParameters parameters) 
		{
			

			ValidateParameters(parameters);

			var ageThreeCardsInHand = parameters.TargetPlayer.Hand.Where(x => x.Age == 3).ToList();

			if (ageThreeCardsInHand.Count == 0)
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return a [3] from your hand. If you do, draw three [1].");

			if (!answer.HasValue || !answer.Value)
				return;

			var cardToReturn = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = ageThreeCardsInHand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.RemoveCardFromHand(cardToReturn);
			Return.Action(cardToReturn, parameters.AgeDecks);

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));
			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));
			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

			PlayerActed(parameters);
		}
    }
}