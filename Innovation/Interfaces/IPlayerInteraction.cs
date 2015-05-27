using System.Collections.Generic;
using System.Linq;
using Innovation.Player;

namespace Innovation.Interfaces
{
	public interface IPlayerInteraction
	{
		AskQuestion AskQuestionHandler { get; set; }
		PickCards PickCardsHandler { get; set; }
		PickColor PickColorHandler { get; set; }
		PickPlayer PickPlayerHandler { get; set; }
		RevealCard RevealCardHandler { get; set; }
		OrderCards OrderCardsHandler { get; set; }
		
		bool? AskQuestion(string playerId, string question);
		bool? RevealCard(string playerId, ICard card);
		Color PickColor(string playerId, IEnumerable<Color> colors);
		IEnumerable<ICard> PickCards(string playerId, PickCardParameters parameters);
		IPlayer PickPlayer(string playerId, IEnumerable<IPlayer> players);
		IEnumerable<ICard> OrderCards(string playerId, IEnumerable<ICard> cardsToOrder);
	}
}
