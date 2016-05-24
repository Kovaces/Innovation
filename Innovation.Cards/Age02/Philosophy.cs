using System;
using System.Linq;
using System.Collections.Generic;

using Innovation.Actions;
using Innovation.Interfaces;

using Innovation.Player;

namespace Innovation.Cards
{
    public class Philosophy : CardBase
    {
        public override string Name => "Philosophy";
        public override int Age => 2;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may splay left any one color of your cards.", Action1)
            , new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may score a card from your hand.", Action2)
        };

        void Action1(ICardActionParameters parameters) 
		{
			

			ValidateParameters(parameters);

			var colorsToSelectFrom = parameters.TargetPlayer.Tableau.GetTopCards().Select(x => x.Color).ToList();
			if (!colorsToSelectFrom.Any())
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay left any one color of your cards.");
			if (!answer.HasValue || !answer.Value)
				return;

			parameters.TargetPlayer.Tableau.Stacks[parameters.TargetPlayer.Interaction.PickColor(parameters.TargetPlayer.Id, colorsToSelectFrom)].Splay(SplayDirection.Left);
			
			PlayerActed(parameters);
		}

		void Action2(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may score a card from your hand.");
			if (!answer.HasValue || !answer.Value)
				return;

			var card = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			parameters.TargetPlayer.RemoveCardFromHand(card);
			Score.Action(card, parameters.TargetPlayer);

			PlayerActed(parameters);
		}
    }
}