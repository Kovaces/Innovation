using Innovation.Actions;
using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Cards
{
    public class Sailing : CardBase
    {
        public override string Name => "Sailing";
        public override int Age => 1;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Crown, "Draw and meld a [1].", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            Meld.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);

            PlayerActed(parameters);
        }
    }
}