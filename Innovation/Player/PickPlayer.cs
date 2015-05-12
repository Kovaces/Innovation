using System;
using System.Collections.Generic;

namespace Innovation.Player
{
	public class PickPlayer : PlayerAction<Player, string, IEnumerable<Player>>
	{
		public override Action<string, IEnumerable<Player>> Handler { get; set; }
	}
}
