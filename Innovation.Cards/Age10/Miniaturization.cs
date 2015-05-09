using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models.Interfaces;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Players;

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
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you returned a [10] draw a [10] for every different value card in your score pile.", Action1)
                };
            }
        }

	    void Action1(ICardActionParameters input)
	    {
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

		    if (!parameters.TargetPlayer.Hand.Any())
			    return;

			var answer = ((Player)parameters.TargetPlayer).Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return a card from your hand. If you returned a [10] draw a [10] for every different value card in your score pile.");
			if (!answer.HasValue || !answer.Value)
				return;

			var selectedCard = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
			
			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			Return.Action(selectedCard, parameters.AgeDecks);

			if (!selectedCard.Age.Equals(10))
				return;

		    for (var i = 0; i < parameters.TargetPlayer.Tableau.ScorePile.Select(c => c.Age).Distinct().Count(); i++)
			    parameters.TargetPlayer.AddCardToHand(Draw.Action(10, parameters.AgeDecks));

			PlayerActed(parameters);
		}
    }
}