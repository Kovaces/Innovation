using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System.Collections.Generic;

namespace Innovation.Models
{
	//Player
	public delegate void RevealCard(string playerId, ICard card);
	public delegate void PickColorToSplayOutgoing(string playerId, IEnumerable<Color> colorsToSplay, SplayDirection splayDirection);
	public delegate void PickMultipleCardsOutgoing(string playerId, IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect);
	public delegate void AskQuestionOutgoing(string playerId, string question);
	public delegate void PickPlayersOutgoing(string playerId, IEnumerable<IPlayer> playerList, int minimumNumberToSelect, int maximumNumberToSelect);
	public delegate void StartTurn(string playerId);
	public delegate void UpdateClient(string playerId);

	//Game
	public delegate void GameOver(string gameName, string winner);
	public delegate void SyncGameStateDelegate(string gameId);
	public delegate void RevealCardDelegate(string gameId);

	//Card
	public delegate CardActionResults CardActionDelegate(CardActionParameters parameters);
}
