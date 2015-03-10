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
		string Team { get; set; } //the base rules support team play but implementing that is low on the priority list

		int ActionsTaken { get; set; }


		void PickCardFromHand();
		void PickCard(IEnumerable<ICard> cardsToSelectFrom);
		void PickMultipleCards(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect);
		void RevealCard(ICard card);
		void AskToSplay(IEnumerable<Color> colorsToSplay, SplayDirection directionToSplay);
		void AskQuestion(string question);
		void PickPlayer(List<IPlayer> playerList, int minimumNumberToSelect, int maximumNumberToSelect);

		void StartTurn();
	}
}