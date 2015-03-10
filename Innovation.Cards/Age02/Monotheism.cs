using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Actions.Handlers;
namespace Innovation.Cards
{
    public class Monotheism : CardBase
    {
        public override string Name { get { return "Monotheism"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Tower; } }
        public override Symbol Center { get { return Symbol.Tower; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Tower, "Draw and tuck a [1].", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> activePlayerTopColors = parameters.ActivePlayer.Tableau.GetStackColors();
			List<ICard> possibleTransferCards = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => !activePlayerTopColors.Contains(x.Color)).ToList();

			if (possibleTransferCards.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				possibleTransferCards,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			ICard cardToTransfer = parameters.Answer.SingleCard;
			if (cardToTransfer == null)
				throw new ArgumentNullException("Must choose a card.");

			parameters.TargetPlayer.Tableau.Stacks[cardToTransfer.Color].RemoveCard(cardToTransfer);
			parameters.ActivePlayer.Tableau.ScorePile.Add(cardToTransfer);

			Tuck.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}


        CardActionResults Action2(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			Tuck.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}
    }
}