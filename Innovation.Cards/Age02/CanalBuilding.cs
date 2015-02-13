using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
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
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Crown, "You may exchange all the highest cards in your hand with all the highest cards in your score pile.", Action1)
                };
            }
        }
		bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<ICard> cardsInPileToTransfer = new List<ICard>();
			List<ICard> cardsInHandToTransfer = new List<ICard>();

			if (parameters.TargetPlayer.Hand.Count > 0)
			{
				int maxAgeInHand = parameters.TargetPlayer.Hand.Max(x => x.Age);
				cardsInHandToTransfer = parameters.TargetPlayer.Hand.Where(x => x.Age == maxAgeInHand).ToList();
			}
			if (parameters.TargetPlayer.Tableau.ScorePile.Count > 0)
			{
				int maxAgeInPile = parameters.TargetPlayer.Tableau.ScorePile.Max(x => x.Age);
				cardsInPileToTransfer = parameters.TargetPlayer.Tableau.ScorePile.Where(x => x.Age == maxAgeInPile).ToList();
			}

			foreach (ICard card in cardsInHandToTransfer)
			{
				parameters.TargetPlayer.Hand.Remove(card);
				parameters.TargetPlayer.Tableau.ScorePile.Add(card);
			}
			foreach (ICard card in cardsInPileToTransfer)
			{
				parameters.TargetPlayer.Tableau.ScorePile.Remove(card);
				parameters.TargetPlayer.Hand.Add(card);
			}
			
			if (cardsInHandToTransfer.Count + cardsInPileToTransfer.Count > 0)
				return true;

			return false;
		}
    }
}