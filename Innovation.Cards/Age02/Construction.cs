using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Interfaces;
using Innovation.Models.Enums;
using Innovation.Players;

namespace Innovation.Cards
{
    public class Construction : CardBase
    {
        public override string Name { get { return "Construction"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Red; } }
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
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer two cards from your hand to my hand! Draw a [2]!", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Tower, "If you are the only player with five top cards, claim the Empire achievement.", Action2)
                };
            }
        }
		void Action1(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Any())
			{
				var selectedCards = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = Math.Min(parameters.TargetPlayer.Hand.Count, 2), MaximumCardsToPick = 2 });
				foreach (var card in selectedCards)
				{
					parameters.TargetPlayer.RemoveCardFromHand(card);
					parameters.ActivePlayer.AddCardToHand(card);
				}

				parameters.TargetPlayer.AddCardToHand(Draw.Action(2, parameters.AgeDecks));
			}
		}
		

        void Action2(ICardActionParameters input) 
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			var playersWithFiveTopCards = parameters.Players.Where(p => p.Tableau.GetStackColors().Count == 5).ToList();
			
			if (playersWithFiveTopCards.Count != 1)
				return;

	        if (playersWithFiveTopCards.First() != parameters.TargetPlayer)
		        return;
			
			throw new NotImplementedException("Empire Achievement"); // TODO::achieve Empire.  Special achievements need a larger framework and some discussion
			PlayerActed(parameters);
		}
    }
}