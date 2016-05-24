using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Emancipation : CardBase
    {
        public override string Name => "Emancipation";
        public override int Age => 6;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a card from your hand to my score pile! If you do, draw a [6]!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red or puple cards right.", Action2)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}