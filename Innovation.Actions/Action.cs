using Innovation.Models;
using Innovation.Models.Interfaces;

namespace Innovation.Actions
{
	public interface IAction
	{
		Game Game { get; set; }
		IPlayer ActivePlayer { get; set; }
		ICard Card { get; set; }

		ICard Perform();
	}

	public class Action<T> where T : IAction, new()
	{
		private readonly T _actionType;

		public Action(Game game, IPlayer player, ICard card)
		{
			_actionType = new T { Game = game, ActivePlayer = player, Card = card, };
		}

		public ICard Perform()
		{
			return (_actionType.Game.GameEnded) ? null :_actionType.Perform();
		}
	}
}
