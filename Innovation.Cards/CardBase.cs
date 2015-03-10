using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
	public abstract class CardBase : ICard
	{
		public string Id { get { return "C_" + new Regex("[^a-zA-Z0-9]").Replace(Name, ""); } }

		public abstract Symbol Top { get; }
		public abstract Symbol Left { get; }
		public abstract Symbol Center { get; }
		public abstract Symbol Right { get; }

		public abstract string Name { get; }
		public abstract int Age { get; }
		public abstract Color Color { get; }
		public abstract IEnumerable<CardAction> Actions { get; }

		public bool HasSymbol(Symbol symbol)
		{
			return ((Top == symbol) || (Left == symbol) || (Center == symbol) || (Right == symbol));
		}

		//Protected Properties and Methods
		protected void ValidateParameters(CardActionParameters parameters)
		{
			if (parameters.TargetPlayer == null)
				throw new ArgumentOutOfRangeException("parameters", "Target player cannot be null");
			
			if (parameters.Game == null)
				throw new ArgumentOutOfRangeException("parameters", "Game cannot be null");

			if (parameters.ActivePlayer == null)
				throw new ArgumentOutOfRangeException("parameters", "Active player cannot be null");
			
			if (parameters.PlayerSymbolCounts == null)
				throw new ArgumentOutOfRangeException("parameters", "Player Symbol Counts cannot be null");
		}
	}
}
