using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
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
		public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Tower, "You may meld any number of cards from your hand, each with a [TOWER]. If you melded four or more cards in this way, claim the Monument achievement.", Action1)
                };
            }
        }

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

	        var cardsWithTowers = parameters.TargetPlayer.Hand.Where(c => c.HasSymbol(Symbol.Tower)).ToList();
			
			if (cardsWithTowers.Count == 0)
				return false;

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.TargetPlayer,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				cardsWithTowers,
				0, cardsWithTowers.Count,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return false;
		}
		bool Action1_Step2(CardActionParameters parameters)
		{
			var selectedCards = parameters.Answer.MultipleCards;
			foreach (var card in selectedCards)
			{
				parameters.TargetPlayer.Hand.Remove(card);
				Meld.Action(card, parameters.TargetPlayer);
			}

			if (selectedCards.Count > 4)
				throw new NotImplementedException("Monument Achievement"); // TODO::achieve Monument.  Special achievements need a larger framework and some discussion

			return (selectedCards.Count > 0);
		}
    }
}