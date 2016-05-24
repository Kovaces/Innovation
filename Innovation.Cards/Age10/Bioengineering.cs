using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Game;
using Innovation.Interfaces;

using Innovation.Models.Other;

using Innovation.Player;
using Innovation.Storage;

namespace Innovation.Cards
{
    public class Bioengineering : CardBase
    {
        public override string Name => "Bioengineering";
        public override int Age => 10;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Clock,"Transfer a top card with a [LEAF] from any other player's board to your score pile.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Clock,"If any player has fewer than three [LEAF] on their board, the single player with the most [LEAF] on their board wins.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            var transferCards = parameters.Players.SelectMany(p => p.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Leaf))).ToList();
            if (transferCards.Count == 0)
                return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = transferCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

            parameters.Players.First(p => p.Tableau.Stacks[selectedCard.Color].Cards.Contains(selectedCard)).Tableau.Stacks[selectedCard.Color].RemoveCard(selectedCard);
            parameters.TargetPlayer.Tableau.Stacks[selectedCard.Color].AddCardToTop(selectedCard);

            PlayerActed(parameters);
        }

        void Action2(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            if (parameters.Players.ToList().Exists(p => p.Tableau.GetSymbolCount(Symbol.Leaf) < 3))
            {
                parameters.AddToStorage("WinnerKey", parameters.Players.OrderByDescending(p => p.Tableau.GetSymbolCount(Symbol.Leaf)).ToList().First());
                throw new EndOfGameException();
            }
        }
    }
}