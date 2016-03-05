using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Cards.Tests
{
	[TestClass]
	public class CardListTests
	{
		[TestMethod]
		public void CardListTests_VerifyReflection()
		{
			var cardList = CardList.GetCardList().ToList();

			var age1 = cardList.Where(c => c.Age == 1).ToList();
			Assert.AreEqual(14, age1.Count());

			var age2 = cardList.Where(c => c.Age == 2).ToList();
			Assert.AreEqual(10, age2.Count());

			var age3 = cardList.Where(c => c.Age == 3).ToList();
			Assert.AreEqual(10, age3.Count());

			var age4 = cardList.Where(c => c.Age == 4).ToList();
			Assert.AreEqual(10, age4.Count());

			var age5 = cardList.Where(c => c.Age == 5).ToList();
			Assert.AreEqual(10, age5.Count());

			var age6 = cardList.Where(c => c.Age == 6).ToList();
			Assert.AreEqual(10, age6.Count());

			var age7 = cardList.Where(c => c.Age == 7).ToList();
			Assert.AreEqual(10, age7.Count());

			var age8 = cardList.Where(c => c.Age == 8).ToList();
			Assert.AreEqual(10, age8.Count());

			var age9 = cardList.Where(c => c.Age == 9).ToList();
			Assert.AreEqual(10, age9.Count());

			var age10 = cardList.Where(c => c.Age == 10).ToList();
			Assert.AreEqual(10, age10.Count());
		}
	}
}
