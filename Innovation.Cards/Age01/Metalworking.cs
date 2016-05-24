using Innovation.Actions;

using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Cards
{
    public class Metalworking : CardBase
    {
        public override string Name => "Metalworking";
        public override int Age => 1;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Tower;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Tower, "Draw and reveal a [1]. If it has a [TOWER], score it and repeat this dogma effect. Otherwise, keep it.", Action1)
        };

        void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			var drawnCard = DrawAndReveal(parameters, 1);
			
			while (drawnCard.HasSymbol(Symbol.Tower))
			{
				Score.Action(drawnCard, parameters.TargetPlayer);

				drawnCard = DrawAndReveal(parameters, 1);
			}

			parameters.TargetPlayer.AddCardToHand(drawnCard);

			PlayerActed(parameters);
		}
    }
}