using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Gunpowder : CardBase
    {
        public override string Name { get { return "Gunpowder"; } }
        public override int Age { get { return 4; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a top card with a [TOWER] from your board to my score pile!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"If any card was transferred due to the demand, draw and score a [2].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}