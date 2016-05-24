using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
	public class Feudalism : CardBase
	{
        public override string Name => "Feudalism";
        public override int Age => 3;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer a card with a [TOWER] from your hand to my hand!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Tower,"You may splay your yellow or purple cards left.", Action2)
        };


		void Action1(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var towerCards = parameters.TargetPlayer.Hand.Where(c => c.HasSymbol(Symbol.Tower)).ToList();

			if (!towerCards.Any())
				return;

			var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters {CardsToPickFrom = towerCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1}).First();

			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			parameters.ActivePlayer.AddCardToHand(selectedCard);
		}

		void Action2(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var validColors = new List<Color>();
			
			if (parameters.TargetPlayer.Tableau.Stacks[Color.Purple].Cards.Count > 1)
				validColors.Add(Color.Purple);

			if (parameters.TargetPlayer.Tableau.Stacks[Color.Purple].Cards.Count > 1)
				validColors.Add(Color.Yellow);

			if (!validColors.Any())
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your yellow or purple cards left.");
			if (!answer.HasValue || !answer.Value)
				return;

			var selectedColor = parameters.TargetPlayer.Interaction.PickColor(parameters.TargetPlayer.Id, validColors);

			parameters.TargetPlayer.SplayStack(selectedColor, SplayDirection.Left);

			PlayerActed(parameters);
		}
	}
}