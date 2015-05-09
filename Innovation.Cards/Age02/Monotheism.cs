using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Models.Interfaces;
using Innovation.Players;

namespace Innovation.Cards
{
    public class Monotheism : CardBase
    {
        public override string Name { get { return "Monotheism"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Tower; } }
        public override Symbol Center { get { return Symbol.Tower; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Tower, "Draw and tuck a [1].", Action2)
                };
            }
        }
        void Action1(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			var activePlayerTopColors = parameters.ActivePlayer.Tableau.GetStackColors();
			var possibleTransferCards = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => !activePlayerTopColors.Contains(x.Color)).ToList();

			if (possibleTransferCards.Count == 0)
				return;

			var cardToTransfer = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = possibleTransferCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.Tableau.Stacks[cardToTransfer.Color].RemoveCard(cardToTransfer);
			parameters.ActivePlayer.Tableau.ScorePile.Add(cardToTransfer);

			Tuck.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);
		}

		void Action2(ICardActionParameters input) 
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			Tuck.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
    }
}