using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public class Request
	{
		public RequestType Type { get; set; }
		public IPlayer TargetPlayer { get; set; }
		public IPlayer ActivePlayer { get; set; }
		public object Objects { get; set; }
		public int MinimumNumberToSelect { get; set; }
		public int MaximumNumberToSelect { get; set; }
		public Dictionary<IPlayer, Dictionary<Symbol, int>> PlayerSymbolCounts { get; set; }

		public CardActionDelegate ResponseHandler { get; set; }
	}
}
