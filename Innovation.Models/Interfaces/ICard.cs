using System.Collections.Generic;
using Innovation.Models.Enums;

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
		IEnumerable<CardAction> Actions { get; }

		bool HasSymbol(Symbol symbol);
	}
}
