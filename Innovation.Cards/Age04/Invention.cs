using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Invention : ICard
    {
        public string Name { get { return "Invention"; } }
        public int Age { get { return 4; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay right any one color of your cards currently splayed left. If you do, draw and score a [4].", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If you have five colors splayed, each in any direction, claim the Wonder achievement.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}