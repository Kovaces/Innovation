﻿using System.Collections.Generic;
using Innovation.Models.Enums;

namespace Innovation.Models
{
	public interface ICard
	{
		Symbol Top { get; }
		Symbol Left { get; }
		Symbol Center { get; }
		Symbol Right { get; }

		int Age { get; }
		Color Color { get; }
		IEnumerable<CardAction> Actions { get; }
	}
}
