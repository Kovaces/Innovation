using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Bioengineering : ICard
    {
        public string Name { get { return "Bioengineering"; } }
        public int Age { get { return 10; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Transfer a top card with a [LEAF] from any other player's board to your score pile.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"If any player has fewer than three [LEAF] on their board, the single player with the most [LEAF] on their board wins.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}