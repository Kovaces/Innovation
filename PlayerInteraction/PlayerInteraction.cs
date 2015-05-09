using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System.Collections.Generic;

namespace Innovation.Players
{
	public class PlayerInteraction
	{
		public AskQuestion AskQuestionHandler { get; set; }
		public bool? AskQuestion(string playerId, string question)
		{
			return AskQuestionHandler.Action(playerId, question);
		}

		public RevealCard RevealCardHandler { get; set; }
		public bool? RevealCard(string playerId, ICard card)
		{
			return RevealCardHandler.Action(playerId, card);
		}

		public PickColor PickColorHandler { get; set; }
		public Color PickColor(string playerId, IEnumerable<Color> colors)
		{
			return PickColorHandler.Action(playerId, colors);
		}

		public PickCards PickCardsHandler { get; set; }
		public IEnumerable<ICard> PickCards(string playerId, PickCardParameters parameters)
		{
			return PickCardsHandler.Action(playerId, parameters);
		}

		public PickPlayer PickPlayerHandler { get; set; }
		public IPlayer PickPlayer(string playerId, IEnumerable<Player> players)
		{
			return PickPlayerHandler.Action(playerId, players);
		}
	}
}
