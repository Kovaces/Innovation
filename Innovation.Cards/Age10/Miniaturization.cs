using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Miniaturization : CardBase
    {
        public override string Name { get { return "Miniaturization"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you returned a [10] draw a [10] for every different value card in your score pile.", Action1)
                };
            }
        }

	    CardActionResults Action1(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}

		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			var selectedCard = parameters.Answer.SingleCard;
			if (selectedCard == null)
				return new CardActionResults(false, false);

			Return.Action(selectedCard, parameters.Game);

			if (!selectedCard.Age.Equals(10))
				return new CardActionResults(true, false);

			for (var i = 0; i < parameters.TargetPlayer.Tableau.ScorePile.Select(c => c.Age).Distinct().Count(); i++)
				Draw.Action(10, parameters.Game);

			return new CardActionResults(true, false);
		}
    }
}