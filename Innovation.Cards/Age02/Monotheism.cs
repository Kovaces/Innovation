using System;
using System.Linq;
using System.Collections.Generic;

using Innovation.Actions;
using Innovation.Interfaces;

using Innovation.Player;

namespace Innovation.Cards
{
    public class Monotheism : CardBase
    {
        public override string Name => "Monotheism";
        public override int Age => 2;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Tower;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!", Action1)
            ,new CardAction(ActionType.Required, Symbol.Tower, "Draw and tuck a [1].", Action2)
        };

        void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			var activePlayerTopColors = parameters.ActivePlayer.Tableau.GetStackColors();
			var possibleTransferCards = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => !activePlayerTopColors.Contains(x.Color)).ToList();

			if (possibleTransferCards.Count == 0)
				return;

			var cardToTransfer = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = possibleTransferCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.Tableau.Stacks[cardToTransfer.Color].RemoveCard(cardToTransfer);
			parameters.ActivePlayer.Tableau.ScorePile.Add(cardToTransfer);

			Tuck.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);
		}

		void Action2(ICardActionParameters parameters) 
		{
			

			ValidateParameters(parameters);

			Tuck.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
    }
}