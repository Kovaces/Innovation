using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class CanalBuilding : ICard
    {
        public string Name { get { return "Canal Building"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
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
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<ICard> cardsInPileToTransfer = new List<ICard>();
			List<ICard> cardsInHandToTransfer = new List<ICard>();

			if (targetPlayer.Hand.Count > 0)
			{
				int maxAgeInHand = targetPlayer.Hand.Max(x => x.Age);
				cardsInHandToTransfer = targetPlayer.Hand.Where(x => x.Age == maxAgeInHand).ToList();
			}
			if (targetPlayer.Tableau.ScorePile.Count > 0)
			{
				int maxAgeInPile = targetPlayer.Tableau.ScorePile.Max(x => x.Age);
				cardsInPileToTransfer = targetPlayer.Tableau.ScorePile.Where(x => x.Age == maxAgeInPile).ToList();
			}

			foreach (ICard card in cardsInHandToTransfer)
			{
				targetPlayer.Hand.Remove(card);
				targetPlayer.Tableau.ScorePile.Add(card);
			}
			foreach (ICard card in cardsInPileToTransfer)
			{
				targetPlayer.Tableau.ScorePile.Remove(card);
				targetPlayer.Hand.Add(card);
			}
			
			if (cardsInHandToTransfer.Count + cardsInPileToTransfer.Count > 0)
				return true;

			return false;
		}
    }
}