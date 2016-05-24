using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
    public class Currency : CardBase
	{
        public override string Name => "Currency";
        public override int Age => 2;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Crown, "You may return any number of cards from your hand. If you do, draw and score a [2] for every different value card you returned.", Action1)
        };

        void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Any())
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return any number of cards from your hand. If you do, draw and score a [2] for every different value card you returned.");
			if (!answer.HasValue || !answer.Value)
				return;

			var cardsToReturn = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = parameters.TargetPlayer.Hand.Count }).ToList();

			foreach (var card in cardsToReturn)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				Return.Action(card, parameters.AgeDecks);
			}

			var differentAges = cardsToReturn.Select(x => x.Age).Distinct().Count();

			for (var i = 0; i < differentAges; i++)
				Score.Action(Draw.Action(2, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
	}
}