using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class ThePirateCode : CardBase
    {
        public override string Name { get { return "The Pirate Code"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer two cards of value [4] or less from your score pile to my score pile!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Crown,"If any card was transferred due to the demand, score the lowest top card with a [CROWN] from your board.", Action2)
                };
            }
        }
        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}