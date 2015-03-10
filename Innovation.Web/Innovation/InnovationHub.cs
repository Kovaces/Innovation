using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Innovation.Cards;
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
		public async void CreateGame(string gameName, string playerId, string[] playerIds)
		{
			await Groups.Add(Context.ConnectionId, gameName);
			_innovation.CreateGame(gameName, playerIds);
		}

		//player interaction
		public void PickCardsResponse(string gameId, string playerId, string[] cardIds)
		{
			_innovation.PickCardsResponse(gameId, playerId, cardIds);
		}

		public void PickPlayerResponse(string gameId, string playerId, string[] selectedPlayers)
		{
			_innovation.PickPlayersResponse(gameId, playerId, selectedPlayers);
		}

		public void AskQuestionResponse(string gameId, string playerId, bool response)
		{
			_innovation.AskQuestionResponse(gameId, playerId, response);
		}









		// data recovery
		public string GetCards()
		{
			var thing = CardList.GetCardList()
							.Select(c => new
							{
								CardId = c.Id,
								Name = c.Name,
								Color = c.Color,
								Age = c.Age,
								Top = c.Top,
								Left = c.Left,
								Center = c.Center,
								Right = c.Right,
								Actions = c.Actions.Select(a => new
								{
									Symbol = a.Symbol,
									ActionType = a.ActionType,
									ActionText = a.ActionText
								}),
								Image = string.Empty
							}).ToList();

			string response = Newtonsoft.Json.JsonConvert.SerializeObject(thing);

			return response;

			//Clients.Client(Context.ConnectionId).populateCardList(response);
		}




		// default chat action
		public void Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(name, message);
		}
	}
}