using Innovation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
    public class Masonry : CardBase
    {
        public override string Name { get { return "Masonry"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Tower; } }
		public override Symbol Left { get { return Symbol.Blank; } }
		public override Symbol Center { get { return Symbol.Tower; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Tower, "You may meld any number of cards from your hand, each with a [TOWER]. If you melded four or more cards in this way, claim the Monument achievement.", Action1)
                };
            }
        }

        void Action1(ICardActionParameters parameters) 
		{
			

			ValidateParameters(parameters);

	        var cardsWithTowers = parameters.TargetPlayer.Hand.Where(c => c.HasSymbol(Symbol.Tower)).ToList();

	        if (cardsWithTowers.Count == 0)
		        return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may meld any number of cards from your hand, each with a [TOWER]. If you melded four or more cards in this way, claim the Monument achievement.");
			
			if (!answer.HasValue || !answer.Value)
				return;

			var selectedCards = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = cardsWithTowers, MinimumCardsToPick = 1, MaximumCardsToPick = cardsWithTowers.Count }).ToList();

			foreach (var card in selectedCards)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				Meld.Action(card, parameters.TargetPlayer);
			}

			if (selectedCards.Count > 4)
				throw new NotImplementedException("Monument Achievement"); // TODO::achieve Monument.  Special achievements need a larger framework and some discussion

			PlayerActed(parameters);
		}

    }
}