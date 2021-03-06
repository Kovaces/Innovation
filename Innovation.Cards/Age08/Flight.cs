﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Flight : CardBase
    {
        public override string Name => "Flight";
        public override int Age => 8;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Blank;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Crown,"If you red cards are splayed up, you may splay any one color of your cards up.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your red cards up.", Action2)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}