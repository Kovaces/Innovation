using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Players
{
	public class AskQuestion : PlayerAction<bool?, string, string>
	{
		public override Action<string, string> Handler { get; set; }
	}
}
