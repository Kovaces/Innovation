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
		string Name { get; set; }
		ITableau Tableau { get; set; }
		List<ICard> Hand { get; set; }
		string Team { get; set; } //the base rules support team play but implementing that is low on the priority list

	    ICard PickCardFromHand();
	    ICard PickCard(IEnumerable<ICard> cardsToSelectFrom);
	    IEnumerable<ICard> PickMultipleCards(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect);
	    void RevealCard(ICard card);
	    bool AskToSplay(Color colorToSplay, SplayDirection directionToSplay);
	    bool AskQuestion(string question);

		IPlayer PickPlayer(List<IPlayer> playerList);
	}
}
