
using System.Collections.Generic;

namespace Innovation.Models
{
	public class Player
	{
		public string Name { get; set; }
		public Tableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list
	}
}
