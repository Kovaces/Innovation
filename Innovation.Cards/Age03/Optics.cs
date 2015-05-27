using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
	public class Optics : CardBase
	{
		public override string Name { get { return "Optics"; } }
		public override int Age { get { return 3; } }
		public override Color Color { get { return Color.Red; } }
		public override Symbol Top { get { return Symbol.Crown; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Crown; } }
		public override Symbol Right { get { return Symbol.Blank; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
					new CardAction(ActionType.Required,Symbol.Crown,"Draw and meld a [3]. If it has a [CROWN], draw and score a [4]. Otherwise, transfer a card from your score pile to the score pile of an opponent with fewer points than you.", Action1)
				};
			}
		}

		void Action1(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			//Draw and meld a [3]
			var drawnCard = Draw.Action(3, parameters.AgeDecks);
			Meld.Action(drawnCard, parameters.TargetPlayer);

			PlayerActed(parameters);

			//If it has a [CROWN], draw and score a [4]
			if (drawnCard.HasSymbol(Symbol.Crown))
			{
				Score.Action(Draw.Action(4, parameters.AgeDecks), parameters.TargetPlayer);
				return;
			}

			//Otherwise, transfer a card from your score pile to the score pile of an opponent with fewer points than you.
			if (!parameters.TargetPlayer.Tableau.ScorePile.Any())
				return;

			var playerScore = parameters.TargetPlayer.Tableau.GetScore();
			var validOpponents = parameters.Players.Where(p => p.Tableau.GetScore() < playerScore).ToList();

			if (!validOpponents.Any())
				return;

			var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters {CardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile, MinimumCardsToPick = 1, MaximumCardsToPick = 1}).First();
			var selectedOpponent = parameters.TargetPlayer.Interaction.PickPlayer(parameters.TargetPlayer.Id, validOpponents);

			parameters.TargetPlayer.RemoveCardFromScorePile(selectedCard);
			selectedOpponent.AddCardToScorePile(selectedCard);
		}
	}
}