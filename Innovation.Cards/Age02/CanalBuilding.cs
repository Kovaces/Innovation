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
		bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 2);

			List<ICard> cardsInPileToTransfer = new List<ICard>();
			List<ICard> cardsInHandToTransfer = new List<ICard>();

			if (TargetPlayer.Hand.Count > 0)
			{
				int maxAgeInHand = TargetPlayer.Hand.Max(x => x.Age);
				cardsInHandToTransfer = TargetPlayer.Hand.Where(x => x.Age == maxAgeInHand).ToList();
			}
			if (TargetPlayer.Tableau.ScorePile.Count > 0)
			{
				int maxAgeInPile = TargetPlayer.Tableau.ScorePile.Max(x => x.Age);
				cardsInPileToTransfer = TargetPlayer.Tableau.ScorePile.Where(x => x.Age == maxAgeInPile).ToList();
			}

			foreach (ICard card in cardsInHandToTransfer)
			{
				TargetPlayer.Hand.Remove(card);
				TargetPlayer.Tableau.ScorePile.Add(card);
			}
			foreach (ICard card in cardsInPileToTransfer)
			{
				TargetPlayer.Tableau.ScorePile.Remove(card);
				TargetPlayer.Hand.Add(card);
			}
			
			if (cardsInHandToTransfer.Count + cardsInPileToTransfer.Count > 0)
				return true;

			return false;
		}
    }
}