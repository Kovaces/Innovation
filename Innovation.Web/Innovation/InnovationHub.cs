using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Innovation.Models;
using Innovation.Models.Interfaces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace Innovation.Web
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
			return base.OnConnected();
		}

		public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
		{
			_innovation.RemoveUser(Context.ConnectionId);
			return base.OnDisconnected(stopCalled);
		} 

		//game state
		public async void CreateGame(string gameName, string[] playerIds)
		{
			await Groups.Add(Context.ConnectionId, gameName);
			_innovation.CreateGame(gameName, playerIds);
		}

		//player interaction
		public void PickCardResponse(Guid transactionId, Guid gameId, string cardName)
		{
			_innovation.PickCardResponse(transactionId, gameId, cardName);
		}


	}
}