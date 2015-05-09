using System.Collections.Generic;

namespace Innovation.Models
{
	public class ContextStorage
	{
		public static string AnotherPlayerTookDogmaActionKey = "AnotherPlayerTookDogmaActionKey";
		public static string OarsCardTransferedKey = "OarsCardTransferedKey";
		public static string MapMakingCardTransferedKey = "MapMakingCardTransferedKey";
		public static string WinnerKey = "WinnerKey";

		private readonly Dictionary<string, object> _storage = new Dictionary<string, object>();

		public void AddToGameStorage(string key, object value)
		{
			if (_storage.ContainsKey(key))
				_storage[key] = value;
			else
				_storage.Add(key, value);
		}

		public object RetrieveFromGameStorage(string key)
		{
			return (_storage.ContainsKey(key)) ? _storage[key] : null;
		}

		public void RemoveFromGameStorage(string key)
		{
			if (_storage.ContainsKey(key))
				_storage.Remove(key);
		}

		public void ClearGameStorage(string key)
		{
			_storage.Clear();
		}
	}
}
