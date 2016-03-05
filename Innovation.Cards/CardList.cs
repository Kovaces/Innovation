using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Innovation.Interfaces;


namespace Innovation.Cards
{
	public static class CardList
	{
		public static IEnumerable<ICard> GetCardList()
		{
			var assembly = Assembly.GetAssembly(typeof(CardBase));
			var cardTypes = assembly.GetTypes().Where(t => t.BaseType == typeof (CardBase)).ToList();
			var retVal = new List<ICard>();
			foreach (var cardType in cardTypes)
			{
				var instance = (ICard) assembly.CreateInstance(cardType.FullName);
				retVal.Add(instance);
			}

			return retVal;
		}
	}
}
