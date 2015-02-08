using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Canning : ICard
    {
        public string Name { get { return "Canning"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Factory,"You may draw and tuck a [6]. If you do, score all your top cards without a [FACTORY]", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your yellow cards right.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}