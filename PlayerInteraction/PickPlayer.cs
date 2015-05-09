using Innovation.Models.Interfaces;
using System;
using System.Collections.Generic;
using Innovation.Models;

namespace Innovation.Players
{
	public class PickPlayer : PlayerAction<Player, string, IEnumerable<Player>>
	{
		public override Action<string, IEnumerable<Player>> Handler { get; set; }
	}
}
