using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Models.Interfaces;
using Innovation.Players;

namespace Innovation.Cards
{
    public class Mapmaking : CardBase
    {
        public override string Name { get { return "Mapmaking"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Demand, Symbol.Crown, "I demand you transfer a [1] from your score pile, if it has any, to my score pile!", Action1)
                    , new CardAction(ActionType.Required, Symbol.Crown, "If any card was transferred due to the demand, draw and score a [1].", Action2)
                };
            }
        }
		void Action1(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			List<ICard> ageOneCardsinScorePile = parameters.TargetPlayer.Tableau.ScorePile.Where(x => x.Age == 1).ToList();
			
			if (ageOneCardsinScorePile.Count == 0)
				return;

			var selectedCard = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = ageOneCardsinScorePile, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.Tableau.ScorePile.Remove(selectedCard);
			parameters.ActivePlayer.Tableau.ScorePile.Add(selectedCard);

			parameters.AddToStorage(ContextStorage.MapMakingCardTransferedKey, true);
		}

		void Action2(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			var cardTransfered = parameters.GetFromStorage(ContextStorage.MapMakingCardTransferedKey);
			if (cardTransfered != null && !(bool)cardTransfered)
				return;
			
			Score.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
    }
}