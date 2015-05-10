using System;
using System.Collections.Generic;
using Innovation.GameObjects;

namespace Innovation.Interfaces
{
	public interface ICardActionParameters
	{
		IPlayer TargetPlayer { get; set; }
		IPlayer ActivePlayer { get; set; }
		IEnumerable<Deck> AgeDecks { get; set; }
		IEnumerable<IPlayer> Players { get; set; }
		Action<string, object> AddToStorage { get; set; }
		Func<string, object> GetFromStorage { get; set; }
	}
}
