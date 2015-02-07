using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Optics : ICard
    {
        public string Name { get { return "Optics"; } }
        public int Age { get { return 3; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Crown; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Crown,"Draw and meld a [3]. If it has a [CROWN], draw and score a [4]. Otherwise, transfer a card from your score pile to the score pile of an opponent with fewer points than you.", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}