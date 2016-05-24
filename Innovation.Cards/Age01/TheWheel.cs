using Innovation.Actions;
using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Cards
{
    public class TheWheel : CardBase
    {
        public override string Name => "The Wheel";
        public override int Age => 1;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Tower;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Tower, "Draw two [1].", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));
            parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

            PlayerActed(parameters);
        }
    }
}