using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class SteamEngine : CardBase
    {
        public override string Name => "Steam Engine";
        public override int Age => 5;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck two [4], then score your bottom yellow card.", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);
            PlayerActed(parameters);

            Tuck.Action(Draw.Action(4, parameters.AgeDecks), parameters.TargetPlayer);
            Tuck.Action(Draw.Action(4, parameters.AgeDecks), parameters.TargetPlayer);

            var bottomYellow = parameters.TargetPlayer.Tableau.Stacks[Color.Yellow].GetBottomCard();
            parameters.TargetPlayer.RemoveCardFromStack(bottomYellow);
            Score.Action(bottomYellow, parameters.TargetPlayer);
        }
    }
}