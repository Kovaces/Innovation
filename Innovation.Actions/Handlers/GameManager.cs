using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innovation.Models;

namespace Innovation.Actions.Handlers
{
	public class GameManager
	{
		private Game _Game = null;
		private const int SLEEP_MS = 200;

		public GameManager(Game game)
		{
			this._Game = game;
		}

		public void MainGameBrain()
		{
			while (_Game.IsRunning)
			{
				if (_Game.IsWaiting)
				{
					Thread.Sleep(SLEEP_MS);
				}
				else if (!this._Game.ActionQueue.IsEmpty)
				{
					this._Game.ActionQueue.PopAction();
				}
				else
				{
					if (this._Game.ActivePlayer.TurnsTaken < 2)
					{
						RequestQueueManager.PickAction(this._Game, this._Game.ActivePlayer);

					}
				}
			}
		}
	}
}
