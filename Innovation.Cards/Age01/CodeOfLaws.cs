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
    public class CodeOfLaws : CardBase
    {
        public override string Name { get { return "Code of Laws"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Leaf; } }

        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.", Action1)
                };
            }
        }

        void Action1(ICardActionParameters input)
        {
            var parameters = input as CardActionParameters;

            ValidateParameters(parameters);

            List<Color> topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
            List<ICard> cardsMatchingBoardColor = parameters.TargetPlayer.Hand.Where(x => topCardColors.Contains(x.Color)).ToList();

            if (!cardsMatchingBoardColor.Any())
                return;

            var answer = ((Player)parameters.TargetPlayer).Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.");

			if (!answer.HasValue || !answer.Value)
				return;

            var selectedCard = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = cardsMatchingBoardColor, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			Tuck.Action(selectedCard, parameters.TargetPlayer);

            PlayerActed(parameters);

            answer = ((Player)parameters.TargetPlayer).Interaction.AskQuestion(parameters.TargetPlayer.Id, "you may splay that color of your cards left.");

			if (!answer.HasValue || !answer.Value)
				return;
				
            parameters.TargetPlayer.SplayStack(selectedCard.Color, SplayDirection.Left);
		}
    }
}