using System;
using Innovation.Interfaces;



namespace Innovation.Cards
{
	public class CardAction : ICardAction
	{
		public ActionType ActionType { get; set; }
		public Symbol Symbol { get; set; }
		public string ActionText { get; set; }
		public Action<ICardActionParameters> ActionHandler { get; set; }

		public CardAction(ActionType actionType, Symbol symbol, string actionText, Action<ICardActionParameters> actionHandler)
		{
			ActionType = actionType;
			Symbol = symbol;
			ActionText = actionText;
			ActionHandler = actionHandler;
		}
	}
}
