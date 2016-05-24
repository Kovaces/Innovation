using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Game;
using Innovation.Interfaces;
using Innovation.Models;
using Innovation.Storage;


namespace Innovation.Actions
{
    public class Dogma
    {
        public static void Action(ICard card, ICardActionParameters actionParameters)
        {
            var playerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>();

            actionParameters.Players.ToList().ForEach(p => playerSymbolCounts.Add(p, p.Tableau.GetSymbolCounts()));

            foreach (var action in card.Actions)
            {
                var activePlayerSymbolCount = playerSymbolCounts[actionParameters.ActivePlayer][action.Symbol];

                foreach (var targetPlayer in actionParameters.Players)
                {
                    var targetPlayerSymbolCount = playerSymbolCounts[targetPlayer][action.Symbol];

                    if (!PlayerEligable(activePlayerSymbolCount, targetPlayerSymbolCount, action.ActionType == ActionType.Demand))
                        continue;

                    actionParameters.TargetPlayer = targetPlayer;
                    action.ActionHandler(actionParameters);
                }
            }

            if ((bool)actionParameters.GetFromStorage("AnotherPlayerTookDogmaActionKey"))
                actionParameters.ActivePlayer.Hand.Add(Draw.Action(actionParameters.ActivePlayer.Tableau.GetHighestAge(), actionParameters.AgeDecks));
        }

        private static bool PlayerEligable(int activePlayerSymbolCount, int targetPlayerSymbolCount, bool isDemand)
        {
            return isDemand ? (activePlayerSymbolCount > targetPlayerSymbolCount) : (targetPlayerSymbolCount >= activePlayerSymbolCount);
        }
    }
}
