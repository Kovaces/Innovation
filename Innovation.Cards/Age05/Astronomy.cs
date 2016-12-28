using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Astronomy : CardBase
    {
        public override string Name => "Astronomy";
        public override int Age => 5;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and reveal a [6]. If the card is green or blue, meld it and repeat this dogma effect.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If all the non-purple top cards on your board are value [6] or higher, claim the Universe achievement.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var card = DrawAndReveal(parameters, 1);

            if (card.Color == Color.Green || card.Color == Color.Blue)
            {
                Meld.Action(card, parameters.TargetPlayer);
                Action1(parameters);
            }

            PlayerActed(parameters);
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (parameters.TargetPlayer.Tableau.GetTopCards().Where(c => c.Color != Color.Purple).All(c => c.Age >= 6))
            {
                throw new NotImplementedException("Universe Achievement"); // TODO::achieve Universe.  Special achievements need a larger framework and some discussion
            }

            PlayerActed(parameters);
        }
    }
}