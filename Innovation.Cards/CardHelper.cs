using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public static class CardHelper
	{
		public static void GetParameters(object[] parameters, out Game game, out Player targetPlayer)
		{
			if (parameters.Length < 2)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Player and Game");

			targetPlayer = parameters[0] as Player;
			if (targetPlayer == null)
				throw new NullReferenceException("Target player cannot be null");

			game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");
		}
		public static void GetParameters(object[] parameters, out Game game, out Player targetPlayer, out Player currentPlayer)
		{
			if (parameters.Length < 3)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Target Player, Game and Active Player");

			targetPlayer = parameters[0] as Player;
			if (targetPlayer == null)
				throw new NullReferenceException("target player cannot be null");

			game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");

			currentPlayer = parameters[2] as Player;
			if (currentPlayer == null)
				throw new NullReferenceException("Active player cannot be null");
		}

		public static bool CardHasSymbol(ICard card, Symbol symbol)
		{
			if (card.Top == symbol)
				return true;
			if (card.Left == symbol)
				return true;
			if (card.Center == symbol)
				return true;
			if (card.Right == symbol)
				return true;
			
			return false;
		}
	}
}
