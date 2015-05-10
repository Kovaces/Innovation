using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
	public class Databases : CardBase
	{
		public override string Name { get { return "Databases"; } }
		public override int Age { get { return 10; } }
		public override Color Color { get { return Color.Green; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Clock; } }
		public override Symbol Center { get { return Symbol.Clock; } }
		public override Symbol Right { get { return Symbol.Clock; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Clock,"I demand you return half (rounded up) of the cards in your score pile!", Action1)
                };
			}
		}

		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Tableau.ScorePile.Any())
				return;

			var numberOfCardsToReturn = (int) Math.Ceiling(parameters.TargetPlayer.Tableau.ScorePile.Count/2.0d);
			var selectedCards = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile, MinimumCardsToPick = numberOfCardsToReturn, MaximumCardsToPick = numberOfCardsToReturn }).ToList();

			selectedCards.ForEach(c => parameters.TargetPlayer.RemoveCardFromScorePile(c));
			selectedCards.ForEach(c => Return.Action(c, parameters.AgeDecks));
		}
	}
}