using Innovation.Models.Enums;

namespace Innovation.Models
{
	public delegate void CardActionDelegate(object[] parameters);

	public class CardAction
	{
		public ActionType ActionType { get; set; }
		public Symbol Symbol { get; set; }
		public string ActionText { get; set; }
		public CardActionDelegate ActionHandler { get; set; }

		public CardAction(ActionType actionType, Symbol symbol, string actionText, CardActionDelegate actionHandler)
		{
			ActionType = actionType;
			Symbol = symbol;
			ActionText = actionText;
			ActionHandler = actionHandler;
		}
	} 
}
