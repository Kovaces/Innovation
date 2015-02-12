using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Construction : ICard
    {
        public string Name { get { return "Construction"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Tower; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
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
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			Player activePlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer, out activePlayer);

			int numberOfCardsToTransfer = Math.Min(targetPlayer.Hand.Count, 2);
			if (numberOfCardsToTransfer > 0)
			{
				List<ICard> cardsToTransfer = targetPlayer.PickFromMultipleCards(targetPlayer.Hand, numberOfCardsToTransfer, numberOfCardsToTransfer);

				foreach (ICard card in cardsToTransfer)
				{
					targetPlayer.Hand.Remove(card);
					activePlayer.Hand.Add(card);
				}
			}

			targetPlayer.Hand.Add(Draw.Action(2, game));

			return true;
		}
        bool Action2(object[] parameters) 
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			int numberTopCardsActivePlayer = 0;
			int maxNumberTopCardsOtherPlayers = 0;
			foreach (Player player in game.Players)
			{
				if (player == targetPlayer)
					numberTopCardsActivePlayer = targetPlayer.Tableau.GetStackColors().Count;
				else
					maxNumberTopCardsOtherPlayers = Math.Max(maxNumberTopCardsOtherPlayers, targetPlayer.Tableau.GetStackColors().Count);
			}

			if (numberTopCardsActivePlayer == 5 && maxNumberTopCardsOtherPlayers < 5)
			{
				// TODO::achieve Empire.  Special achievements need a larger framework and some discussion
				return true;
			}

			return false;
		}
    }
}