using System;
using System.Collections.Generic;
using Innovation.GameObjects;
using Innovation.Interfaces;


namespace Innovation.Actions
{
    public class CardActionParameters : ICardActionParameters
    {
        public IPlayer TargetPlayer { get; set; }
        public IPlayer ActivePlayer { get; set; }
        public IEnumerable<Deck> AgeDecks { get; set; }
        public IEnumerable<IPlayer> Players { get; set; }
        public Action<string, object> AddToStorage { get; set; }
        public Func<string, object> GetFromStorage { get; set; }
    }
}
