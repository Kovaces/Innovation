using System.Collections.Generic;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public delegate bool CardActionDelegate(CardActionParameters parameters);

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

	public class CardActionParameters
	{
		public IPlayer TargetPlayer { get; set; }
		public IPlayer ActivePlayer { get; set; }
		public Game Game { get; set; }
		public Dictionary<IPlayer, Dictionary<Symbol, int>> PlayerSymbolCounts { get; set; }

		public GenericAnswer Answer { get; set; }
	}

	public class GenericAnswer
	{
		public ICard SingleCard { get; set; }
		public List<ICard> MultipleCards { get; set; }
		public bool Boolean { get; set; }
		public List<IPlayer> Players { get; set; }
		public Color Color { get; set; }
	}
}
