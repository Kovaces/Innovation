using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public class QueuedAction
	{
		public QueuedActionType Type { get; set; }
		public Game Game { get; set; }
		public IPlayer TargetPlayer { get; set; }
		public IPlayer ActivePlayer { get; set; }
		public Dictionary<IPlayer, Dictionary<Symbol, int>> PlayerSymbolCounts { get; set; }
							
		public ActionParameters Parameters { get; set; }
	}
}
