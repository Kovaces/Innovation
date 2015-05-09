using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;

namespace Innovation.Players
{
	public class PickCardParameters
	{
		public IEnumerable<ICard> CardsToPickFrom { get; set; }
		public int MinimumCardsToPick { get; set; }
		public int MaximumCardsToPick { get; set; }
	}
}
