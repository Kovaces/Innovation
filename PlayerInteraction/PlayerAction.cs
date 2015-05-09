using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Innovation.Players
{
	public class PlayerAction<T, T1, T2>
	{
		public virtual int ThreadSleepTime { get; set; }
		public virtual Action<T1, T2> Handler { get; set; }
		public virtual T PlayerResponse { get; private set; }

		public T Action(T1 parameter, T2 parameter2)
		{
			PlayerResponse = default(T);

			Handler(parameter, parameter2);
			Task<T> returnTask = Task<T>.Factory.StartNew(() =>
			{
                while (PlayerResponse == null || PlayerResponse.Equals(default(T)))
					Thread.Sleep(ThreadSleepTime);

				return PlayerResponse;
			});

			return returnTask.Result;
		}

		public void Response(T response)
		{
			PlayerResponse = response;
		}
	}
}
