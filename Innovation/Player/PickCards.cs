using System;
using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Player
{
	public class PickCards : PlayerAction<IEnumerable<ICard>, string, PickCardParameters>
	{
		public override Action<string, PickCardParameters> Handler { get; set; }
	}
}
