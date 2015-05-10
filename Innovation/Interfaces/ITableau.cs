using System.Collections.Generic;
using Innovation.GameObjects;
using Innovation.Models;

namespace Innovation.Interfaces
{
    public interface ITableau
    {
		Dictionary<Color, Stack> Stacks { get; set; }
		int NumberOfAchievements { get; set; }
		List<ICard> ScorePile { get; set; }

	    int GetHighestAge();
	    int GetScore();
	    int GetSymbolCount(Symbol symbol);
	    List<Color> GetStackColors();
	    List<ICard> GetTopCards();
	    Dictionary<Symbol, int> GetSymbolCounts();
    }
}
