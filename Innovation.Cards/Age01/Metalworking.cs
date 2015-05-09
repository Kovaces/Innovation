using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System.Collections.Generic;

namespace Innovation.Cards
{
    public class Metalworking : CardBase
    {
        public override string Name { get { return "Metalworking"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Tower; } }
        public override Symbol Left { get { return Symbol.Tower; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Draw and reveal a [1]. If it has a [TOWER], score it and repeat this dogma effect. Otherwise, keep it.", Action1)
                };
            }
        }
		void Action1(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

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