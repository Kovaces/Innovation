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
    public class Globalization : CardBase
    {
        public override string Name => "Globalization";
        public override int Age => 10;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you return a top card with a [LEAF] on your board.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a [6]. If no player has more [LEAF] than [FACTORY] on their board, the single player with the most points wins.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            var topCardsWithLeaves = parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.HasSymbol(Symbol.Leaf)).ToList();
            if (!topCardsWithLeaves.Any())
                return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = topCardsWithLeaves, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
            
            parameters.TargetPlayer.RemoveCardFromStack(selectedCard);
            Return.Action(selectedCard, parameters.AgeDecks);
        }

        void Action2(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            Score.Action(Draw.Action(6, parameters.AgeDecks), parameters.TargetPlayer);

            PlayerActed(parameters);

            if (!parameters.Players.ToList().Exists(p => p.Tableau.GetSymbolCount(Symbol.Leaf) > p.Tableau.GetSymbolCount(Symbol.Factory)))
            {
                parameters.AddToStorage("WinnerKey", parameters.Players.OrderByDescending(p => p.Tableau.GetScore()).ToList().First());
                throw new EndOfGameException();
            }
        }
    }
}