using Innovation.Models.Enums;
using System;
using System.Collections.Generic;

namespace Innovation.Players
{
	public class PickColor : PlayerAction<Color, string, IEnumerable<Color>>
	{
		public override Action<string, IEnumerable<Color>> Handler { get; set; }
	}
}
