using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Innovation.Web.Innovation
{
	public class InnovationHub : Hub
	{
		private readonly Innovation _innovation;

		public InnovationHub() : this(Innovation.Instance){	}
		public InnovationHub(Innovation instance)
		{
			_innovation = instance;
		}

		//signalR ovverides
		public override System.Threading.Tasks.Task OnConnected()
		{
			_innovation.AddUser(Context.ConnectionId);

			Clients.All.broadcastMessage("svrMsg", Context.ConnectionId + " has connected.");
			return base.OnConnected();
		}

		public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
		{
			_innovation.RemoveUser(Context.ConnectionId);

			Clients.All.broadcastMessage("svrMsg", Context.ConnectionId + " has disconnected.");

			return base.OnDisconnected(stopCalled);
		}
		public void ManualDisconnect()
		{
			Clients.All.broadcastMessage("svrMsg", Context.ConnectionId + " has disconnected on purpose.");
			OnDisconnected(true);
		}

		//game state
		public void CreateGame(string gameName, string[] playerIds)
		{
			Clients.All.broadcastMessage("svrMsg", "Creating game " + gameName + " with players " + playerIds.Length + ".");

			foreach (string playerId in playerIds)
			{
				Task waiter = Groups.Add(playerId, gameName);
				while (!waiter.IsCompleted)
					Thread.Sleep(20);
			}

			Clients.Group(gameName).broadcastMessage("grpMsg", "You're in a group! -" + gameName + "-");

			_innovation.CreateGame(gameName, playerIds);
		}

		//player interaction
		public void PickCardsResponse(string gameId, string[] cardIds)
		{
			_innovation.PickCardsResponse(gameId, Context.ConnectionId, cardIds);
		}

		public void PickPlayerResponse(string gameId, string[] selectedPlayers)
		{
			_innovation.PickPlayersResponse(gameId, Context.ConnectionId, selectedPlayers);
		}

		public void AskQuestionResponse(string gameId, bool response)
		{
			_innovation.AskQuestionResponse(gameId, Context.ConnectionId, response);
		}

		// data
		public void AssignName(string name)
		{
			_innovation.SetNameToClient(Context.ConnectionId, name);

			Clients.All.broadcastMessage("svrMsg", Context.ConnectionId + " is now " + name + ".");
			UpdatePlayers();
		}
		public string GetCards()
		{
			Clients.All.broadcastMessage("svrMsg", Context.ConnectionId + " requested cards.");
			return Newtonsoft.Json.JsonConvert.SerializeObject(_innovation.GetCardList());
		}
		public void UpdatePlayers()
		{
			var thing = _innovation.GetPlayerList();

			Clients.All.receivePlayerList(
				Newtonsoft.Json.JsonConvert.SerializeObject(thing)
			);
		}
		
		// default chat action
		public void Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(name, message);
		}
	}
}