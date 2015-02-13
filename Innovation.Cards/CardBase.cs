using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
	public abstract class CardBase : ICard
	{
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
		protected IPlayer TargetPlayer { get; set; }
		protected IPlayer CurrentPlayer { get; set; }
		protected Game Game { get; set; }

		public void ParseParameters(object[] parameters, int expectedLength)
		{
			if (parameters.Length >= 1)
			{
				TargetPlayer = parameters[0] as IPlayer;
				if (TargetPlayer == null)
					throw new NullReferenceException("Target player cannot be null");
			}

			if (parameters.Length >= 2)
			{
				Game = parameters[1] as Game;
				if (Game == null)
					throw new NullReferenceException("Game cannot be null");
			}

			if (parameters.Length >= 3)
			{
				CurrentPlayer = parameters[2] as Player;
				if (CurrentPlayer == null)
					throw new NullReferenceException("Active player cannot be null");
			}
			

			
		}
	}
}
