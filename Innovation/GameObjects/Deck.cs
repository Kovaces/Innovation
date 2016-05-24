using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Extensions;
using Innovation.Interfaces;

namespace Innovation.GameObjects
{
    public class Deck
    {
        //ctor
        public Deck()
        {
            Cards = new List<ICard>();
        }
        public Deck(List<ICard> cards, int age)
        {
            Cards = cards;
            Age = age;
        }

        //properties
        public List<ICard> Cards { get; set; }
        public int Age { get; set; }

        //methods
        public ICard Draw()
        {
            if (!Cards.Any())
                return null;

            var drawnCard = Cards[0];
            Cards.RemoveAt(0);

            return drawnCard;
        }
        public void InsertAtEnd(ICard card)
        {
            if (Cards.Count > 0)
                Cards.Insert(Cards.Count, card);
            else
                Cards.Add(card);
        }
        public void Shuffle()
        {
            Cards.Shuffle(new Random());
        }
    }
}
