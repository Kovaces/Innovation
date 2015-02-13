using Innovation.Models;
using System;
using System.Collections.Generic;
using Innovation.Models.Interfaces;

namespace Innovation
{
	public class Game
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<IPlayer> Players { get; set; }
		public List<Deck> AgeDecks { get; set; }
		public Deck AgeAchievementDeck { get; set; }

		private Dictionary<string, object> _PropertyBag = new Dictionary<string,object>();

		public object GetPropertyBagValue(string key)
		{
			if (_PropertyBag.ContainsKey(key))
				return _PropertyBag[key];

			return string.Empty;
		}
		public void StashPropertyBagValue(string key, object value)
		{
			if (_PropertyBag.ContainsKey(key))
				_PropertyBag[key] = value;
			else
				_PropertyBag.Add(key, value);
		}
		public void ClearPropertyBag()
		{
			_PropertyBag.Clear();
		}
		
		public void TriggerEndOfGame()
		{
			throw new NotImplementedException();
		}
	}
}
