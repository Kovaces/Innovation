using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Innovation.Models
{
	public class RequestQueue
	{
		private readonly List<Request> _requests = new List<Request>();

		public void AddRequest(Request newRequest)
		{
			_requests.Add(newRequest);
		}

		public void RemoveRequest(Request removedRequest)
		{
			_requests.Remove(removedRequest);
		}

		public Request PopRequestForPlayerByType(IPlayer player, RequestType type)
		{
			var request = _requests.First(x => x.TargetPlayer == player && x.Type == type);
			if (request == null)
				return null;

			_requests.Remove(request);
			return request;
		}

		public bool HasPendingRequests()
		{
			return _requests.Any();
		}
	}
}