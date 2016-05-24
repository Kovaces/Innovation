using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
	public class Machinery : CardBase
	{
        public override string Name => "Machinery";
        public override int Age => 3;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange all the cards in your hand with all the highest cards in my hand!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Leaf,"Score a card from your hand with a [TOWER]. You may splay your red cards left.", Action2)
        };


		void Action1(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var yourCards = parameters.TargetPlayer.Hand;
			var myHighestCards = parameters.ActivePlayer.Hand.Where(c => c.Age.Equals(parameters.ActivePlayer.Hand.Max(d => d.Age)));

			foreach (var card in yourCards)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				parameters.ActivePlayer.AddCardToHand(card);
			}

			foreach (var card in myHighestCards)
			{
				parameters.TargetPlayer.AddCardToHand(card);
				parameters.ActivePlayer.RemoveCardFromHand(card);
			}
		}

		void Action2(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var towerCards = parameters.TargetPlayer.Hand.Where(c => c.HasSymbol(Symbol.Tower)).ToList();

			if (towerCards.Any())
			{
				var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters {CardsToPickFrom = towerCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1}).First();
				parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
				parameters.TargetPlayer.AddCardToScorePile(selectedCard);
				PlayerActed(parameters);
			}

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your red cards left.");
			if (!answer.HasValue || !answer.Value)
				return;

			PlayerActed(parameters);

			parameters.TargetPlayer.SplayStack(Color.Red, SplayDirection.Left);
		}
	}
}