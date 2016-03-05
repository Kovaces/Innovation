using Innovation.Actions;

using System.Collections.Generic;
using System.Linq;
using Innovation.Game;
using Innovation.Interfaces;

using Innovation.Player;
using Innovation.Storage;

namespace Innovation.Cards
{
    public class Oars : CardBase
    {
        public override string Name { get { return "Oars"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Tower; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a card with a [CROWN] from your hand to my score pile! If you do, draw a [1]!", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Tower, "If no cards were transferred due to this demand, draw a [1].", Action2)
                };
            }
        }
        
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			var cardsWithCrowns = parameters.TargetPlayer.Hand.Where(x => x.HasSymbol(Symbol.Crown)).ToList();

            if (cardsWithCrowns.Count == 0)
				return;

			var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = cardsWithCrowns, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			Score.Action(selectedCard, parameters.ActivePlayer);

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

            parameters.AddToStorage("OarsCardTransferedKey", true);
		}


		void Action2(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

            var oarsCardTransfered = parameters.GetFromStorage("OarsCardTransferedKey");
			if (oarsCardTransfered != null && (bool)oarsCardTransfered)
				return;

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

			PlayerActed(parameters);
		}
    }
}