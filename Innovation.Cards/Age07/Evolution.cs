﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Publications : CardBase
    {
        public override string Name => "Publications";
        public override int Age => 7;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may rearrange the order of one color of cards on your board.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your yellow or blue cards up.", Action2)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}