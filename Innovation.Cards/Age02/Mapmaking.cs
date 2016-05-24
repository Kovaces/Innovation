using System;
using System.Linq;
using System.Collections.Generic;

using Innovation.Actions;
using Innovation.Game;
using Innovation.Interfaces;

using Innovation.Player;
using Innovation.Storage;

namespace Innovation.Cards
{
    public class Mapmaking : CardBase
    {
        public override string Name => "Mapmaking";
        public override int Age => 2;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Demand, Symbol.Crown, "I demand you transfer a [1] from your score pile, if it has any, to my score pile!", Action1)
            , new CardAction(ActionType.Required, Symbol.Crown, "If any card was transferred due to the demand, draw and score a [1].", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            List<ICard> ageOneCardsinScorePile = parameters.TargetPlayer.Tableau.ScorePile.Where(x => x.Age == 1).ToList();
            
            if (ageOneCardsinScorePile.Count == 0)
                return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = ageOneCardsinScorePile, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

            parameters.TargetPlayer.Tableau.ScorePile.Remove(selectedCard);
            parameters.ActivePlayer.Tableau.ScorePile.Add(selectedCard);

            parameters.AddToStorage("MapMakingCardTransferedKey", true);
        }

        void Action2(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            var cardTransfered = parameters.GetFromStorage("MapMakingCardTransferedKey");
            if (cardTransfered != null && !(bool)cardTransfered)
                return;
            
            Score.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);

            PlayerActed(parameters);
        }
    }
}