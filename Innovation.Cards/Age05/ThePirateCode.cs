using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class ThePirateCode : ICard
    {
        public string Name { get { return "The Pirate Code"; } }
        public int Age { get { return 5; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Crown; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer two cards of value [4] or less from your score pile to my score pile!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Crown,"If any card was transferred due to the demand, score the lowest top card with a [CROWN] from your board.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}