using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.GameObjects;
using Innovation.Interfaces;


namespace Innovation.Actions
{
    public class Return
    {
        public static void Action(ICard card, IEnumerable<Deck> ageDecks)
        {
            ageDecks.First(x => x.Age == card.Age).InsertAtEnd(card);
        }
    }
}
