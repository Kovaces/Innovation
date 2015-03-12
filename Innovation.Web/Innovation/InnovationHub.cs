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
		public string GetCards()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(_innovation.GetCardList());
		}
		
		// default chat action
		public void Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(name, message);
		}
	}
}