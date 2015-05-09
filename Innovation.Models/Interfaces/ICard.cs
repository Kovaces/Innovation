using System.Collections.Generic;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public interface ICard
	{
		string Id { get; }

		Symbol Top { get; }
		Symbol Left { get; }
		Symbol Center { get; }
		Symbol Right { get; }

		string Name { get; }
		int Age { get; }
		Color Color { get; }
		IEnumerable<ICardAction> Actions { get; }

		bool HasSymbol(Symbol symbol);
	}
}
