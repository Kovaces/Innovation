using System;
using System.Collections.Generic;
using Innovation.Interfaces;

namespace Innovation.Player
{
	public class PickPlayer : PlayerAction<IPlayer, string, IEnumerable<IPlayer>>
	{
		public override Action<string, IEnumerable<IPlayer>> Handler { get; set; }
	}
}
