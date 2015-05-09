using Innovation.Models;
using System;

namespace Innovation.Players
{
	public class RevealCard : PlayerAction<bool?, string, ICard>
	{
		public override Action<string, ICard> Handler { get; set; }
	}
}
