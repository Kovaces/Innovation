using System;
using System.Linq;
using System.Collections.Generic;

using Innovation.Actions;
using Innovation.Interfaces;

using Innovation.Player;

namespace Innovation.Cards
{
    public class CodeOfLaws : CardBase
    {
        public override string Name => "Code of Laws";
        public override int Age => 1;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Crown,"You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            List<Color> topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
            List<ICard> cardsMatchingBoardColor = parameters.TargetPlayer.Hand.Where(x => topCardColors.Contains(x.Color)).ToList();

            if (!cardsMatchingBoardColor.Any())
                return;

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.");

            if (!answer.HasValue || !answer.Value)
                return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = cardsMatchingBoardColor, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
            
            parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
            Tuck.Action(selectedCard, parameters.TargetPlayer);

            PlayerActed(parameters);

            AskToSplay(parameters, selectedCard.Color, SplayDirection.Left);
        }
    }
}