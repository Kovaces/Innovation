using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
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
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Clock,"I demand you return half (rounded up) of the cards in your score pile!", Action1)
                };
			}
		}

		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Tableau.ScorePile.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Tableau.ScorePile,
				(int)Math.Ceiling(parameters.TargetPlayer.Tableau.ScorePile.Count / 2.0d),
				(int)Math.Ceiling(parameters.TargetPlayer.Tableau.ScorePile.Count / 2.0d),
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			var selectedCards = parameters.Answer.MultipleCards;
			if (selectedCards.Count == 0)
				throw new ArgumentNullException("Must choose at least one card.");

			selectedCards.ForEach(c => Return.Action(c, parameters.Game));

			return new CardActionResults(true, false);
		}
	}
}