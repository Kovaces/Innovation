using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public class ActionQueue
	{
		private List<QueuedAction> _Actions { get; set; }

		public void Clear()
		{
			this._Actions.Clear();
		}

		public bool IsEmpty
		{
			get
			{
				return this._Actions.Any();
			}
		}
		public IPlayer ActivePlayer { get; set; }

		public void AddAction(QueuedAction newAction)
		{
			this._Actions.Add(newAction);
		}

		public void AddActionToTop(QueuedAction newAction)
		{
			this._Actions.Insert(0, newAction);
		}

		public void RemoveAction(QueuedAction removedAction)
		{
			this._Actions.Remove(removedAction);
		}

		public QueuedAction PopAction()
		{
			if (this._Actions.Count == 0)
				return null;

			QueuedAction queuedAction = this._Actions.First();
			this._Actions.Remove(queuedAction);
			return queuedAction;
		}
	}

	public class ActionParameters
	{
		public string Text { get; set; }
		public List<ICard> Cards { get; set; }
		public List<IPlayer> Players { get; set; }
		public int MinSelections { get; set; }
		public int MaxSelections { get; set; }
		public CardActionDelegate ResponseHandler { get; set; }
	}
}


