using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Engineering : ICard
    {
        public string Name { get { return "Engineering"; } }
        public int Age { get { return 3; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer all top cards with a [TOWER] from your board to my score pile!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Tower,"You may splay your red cards left.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}