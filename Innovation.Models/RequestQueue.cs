using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;

namespace Innovation.Models.Interfaces
{
	public class RequestQueue
	{
		private List<Request> _Requests { get; set; }

		public void AddRequest(Request newRequest)
		{
			this._Requests.Add(newRequest);
		}

		public void RemoveRequest(Request removedRequest)
		{
			this._Requests.Remove(removedRequest);
		}

		public Request PopRequestForPlayerByType(IPlayer player, RequestType type)
		{
			var request = this._Requests.Where(x => x.TargetPlayer == player && x.Type == type).First();
			if (request == null)
				return null;

			this._Requests.Remove(request);
			return request;
		}
	}
}