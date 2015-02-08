using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Oars : ICard
    {
        public string Name { get { return "Oars"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer a card with a [CROWN] from your hand to my score pile! If you do, draw a [1]!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Tower,"If no cards were transfered due to this demand, draw a [1].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}