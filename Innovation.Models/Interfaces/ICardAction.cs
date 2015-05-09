using Innovation.Models.Enums;
using System;

namespace Innovation.Models.Interfaces
{
	public interface ICardAction
	{
		ActionType ActionType { get; set; }
		Symbol Symbol { get; set; }
		string ActionText { get; set; }
		Action<ICardActionParameters> ActionHandler { get; set; }
	}
}
