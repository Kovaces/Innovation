using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Fission : ICard
    {
        public string Name { get { return "Fission"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Clock; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Clock,"I demand you draw a [10]! If it is red, remove all hands, boards, and score piles from the game! If this occurs, the dogma action is complete.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Return a top card other than Fission from any player's board.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}