using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Player
{
    public class PickCardParameters
    {
        public IEnumerable<ICard> CardsToPickFrom { get; set; }
        public int MinimumCardsToPick { get; set; }
        public int MaximumCardsToPick { get; set; }
    }
}
