using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

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
        public override IEnumerable<CardAction> Actions
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
		bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			int numberOfCardsToTransfer = Math.Min(parameters.TargetPlayer.Hand.Count, 2);
			if (numberOfCardsToTransfer > 0)
			{
				List<ICard> cardsToTransfer = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Hand, numberOfCardsToTransfer, numberOfCardsToTransfer).ToList();

				foreach (ICard card in cardsToTransfer)
				{
					parameters.TargetPlayer.Hand.Remove(card);
					parameters.ActivePlayer.Hand.Add(card);
				}
			}

			parameters.TargetPlayer.Hand.Add(Draw.Action(2, parameters.Game));

			return true;
		}
        bool Action2(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			int numberTopCardsActivePlayer = 0;
			int maxNumberTopCardsOtherPlayers = 0;
			foreach (var player in parameters.Game.Players)
			{
				if (player == parameters.TargetPlayer)
					numberTopCardsActivePlayer = parameters.TargetPlayer.Tableau.GetStackColors().Count;
				else
					maxNumberTopCardsOtherPlayers = Math.Max(maxNumberTopCardsOtherPlayers, parameters.TargetPlayer.Tableau.GetStackColors().Count);
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