using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
	public class CanalBuilding : CardBase
	{
		public override string Name { get { return "Canal Building"; } }
		public override int Age { get { return 2; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Leaf; } }
		public override Symbol Right { get { return Symbol.Crown; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Crown, "You may exchange all the highest cards in your hand with all the highest cards in your score pile.", Action1)
                };
			}
		}
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any() && !parameters.TargetPlayer.Tableau.ScorePile.Any())
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may exchange all the highest cards in your hand with all the highest cards in your score pile.");
			if (!answer.HasValue || !answer.Value)
				return;

			int maxAgeInHand = parameters.TargetPlayer.Hand.Any() ? parameters.TargetPlayer.Hand.Max(x => x.Age) : 0;
			var cardsInHandToTransfer = parameters.TargetPlayer.Hand.Where(x => x.Age == maxAgeInHand).ToList();

			int maxAgeInPile = parameters.TargetPlayer.Tableau.ScorePile.Any() ? parameters.TargetPlayer.Tableau.ScorePile.Max(x => x.Age) : 0;
			var cardsInPileToTransfer = parameters.TargetPlayer.Tableau.ScorePile.Where(x => x.Age == maxAgeInPile).ToList();

			foreach (ICard card in cardsInHandToTransfer)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				parameters.TargetPlayer.Tableau.ScorePile.Add(card);
			}

			foreach (ICard card in cardsInPileToTransfer)
			{
				parameters.TargetPlayer.Tableau.ScorePile.Remove(card);
				parameters.TargetPlayer.AddCardToHand(card);
			}

			PlayerActed(parameters);
		}
	}
}