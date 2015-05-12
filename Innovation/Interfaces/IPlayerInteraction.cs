using System.Collections.Generic;
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

        bool? AskQuestion(string playerId, string question);
        bool? RevealCard(string playerId, ICard card);
        Color PickColor(string playerId, IEnumerable<Color> colors);
        IEnumerable<ICard> PickCards(string playerId, PickCardParameters parameters);
        IPlayer PickPlayer(string playerId, IEnumerable<Player.Player> players);
    }
}
