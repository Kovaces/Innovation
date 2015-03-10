using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Actions.Handlers;
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
        public override IEnumerable<CardAction> Actions
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
        
		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<ICard> cardsWithCrowns = parameters.TargetPlayer.Hand.Where(x => x.HasSymbol(Symbol.Crown)).ToList();

            if (cardsWithCrowns.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				cardsWithCrowns,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			ICard card = parameters.Answer.SingleCard;
			if (card == null)
				throw new ArgumentNullException("Must choose card.");

			parameters.TargetPlayer.Hand.Remove(card);
			Score.Action(card, parameters.ActivePlayer);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			parameters.TargetPlayer.Hand.Add(drawnCard);

            parameters.Game.StashPropertyBagValue("OarsAction1Taken", true);

			return new CardActionResults(true, false);
		}

		CardActionResults Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if ((bool)parameters.Game.GetPropertyBagValue("OarsAction1Taken"))
				return new CardActionResults(false, false);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			parameters.TargetPlayer.Hand.Add(drawnCard);

			return new CardActionResults(true, false);
		}
    }
}