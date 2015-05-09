using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;

namespace Innovation.Players
{
	public class PickCards : PlayerAction<IEnumerable<ICard>, string, PickCardParameters>
	{
		public override Action<string, PickCardParameters> Handler { get; set; }
	}
}
