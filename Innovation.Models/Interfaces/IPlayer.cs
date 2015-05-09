using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;

namespace Innovation.Models.Interfaces
{
	public interface IPlayer
	{
		string Id { get; set; }
		string Name { get; set; }
		ITableau Tableau { get; set; }
		List<ICard> Hand { get; set; }
		string Team { get; set; }
		int ActionsTaken { get; set; }
		int Achievements { get; set; }

        Action<string> UpdateClientHandler { get; set; }
			
		void AddCardToHand(ICard card);
		void RemoveCardFromHand(ICard card);
		void AddCardToStack(ICard card);
		void RemoveCardFromStack(ICard card);
		void TuckCard(ICard card);
		void SplayStack(Color stackColor, SplayDirection direction);
		void AddCardToScorePile(ICard card);
		void RemoveCardFromScorePile(ICard card);
	}
}