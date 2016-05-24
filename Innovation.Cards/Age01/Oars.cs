using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Oars : CardBase
    {
        public override string Name => "Oars";
        public override int Age => 1;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Tower;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a card with a [CROWN] from your hand to my score pile! If you do, draw a [1]!", Action1)
            ,new CardAction(ActionType.Required, Symbol.Tower, "If no cards were transferred due to this demand, draw a [1].", Action2)
        };

        private bool Action1(CardActionParameters parameters)
        {
            ValidateParameters(parameters);

            List<ICard> cardsWithCrowns = parameters.TargetPlayer.Hand.Where(x => x.HasSymbol(Symbol.Crown)).ToList();

            if (cardsWithCrowns.Count == 0)
            {
                parameters.Game.StashPropertyBagValue("OarsAction1Taken", false);
                return false;
            }

            ICard card = parameters.TargetPlayer.PickCard(cardsWithCrowns);
            parameters.TargetPlayer.Hand.Remove(card);
            Score.Action(card, parameters.ActivePlayer);

            parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

            parameters.Game.StashPropertyBagValue("OarsAction1Taken", true);
        
            return true;
        }

        private bool Action2(CardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if ((bool)parameters.Game.GetPropertyBagValue("OarsAction1Taken"))
                return false;
            
            parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));
            
            return true;
        }
    }
}