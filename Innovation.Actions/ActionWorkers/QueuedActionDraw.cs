using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions.ActionWorkers
{
	public static class QueuedActionDraw
	{
		public static void Execute(QueuedAction queuedAction)
		{
			int age = queuedAction.TargetPlayer.Tableau.GetTopCards().Select(x => x.Age).Max();
			ICard drawnCard = Draw.Action(age, queuedAction.Game);
			if (drawnCard != null)
				queuedAction.TargetPlayer.Hand.Add(drawnCard);
		}
	}
}
