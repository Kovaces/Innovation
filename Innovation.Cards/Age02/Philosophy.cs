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
    public class Philosophy : CardBase
    {
        public override string Name { get { return "Philosophy"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may splay left any one color of your cards.", Action1)
                    , new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may score a card from your hand.", Action2)
                };
            }
        }
		void Action1(ICardActionParameters input) 
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			var colorsToSelectFrom = parameters.TargetPlayer.Tableau.GetTopCards().Select(x => x.Color).ToList();
			if (!colorsToSelectFrom.Any())
				return;

			var answer = ((Player)parameters.TargetPlayer).Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay left any one color of your cards.");
			if (!answer.HasValue || !answer.Value)
				return;

			parameters.TargetPlayer.Tableau.Stacks[((Player)parameters.TargetPlayer).Interaction.PickColor(parameters.TargetPlayer.Id, colorsToSelectFrom)].Splay(SplayDirection.Left);
			
			PlayerActed(parameters);
		}

		void Action2(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			var answer = ((Player)parameters.TargetPlayer).Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may score a card from your hand.");
			if (!answer.HasValue || !answer.Value)
				return;

			var card = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			parameters.TargetPlayer.RemoveCardFromHand(card);
			Score.Action(card, parameters.TargetPlayer);

			PlayerActed(parameters);
		}
    }
}