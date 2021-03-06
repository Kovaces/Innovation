﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class PrintingPress : CardBase
    {
        public override string Name => "Printing Press";
        public override int Age => 4;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your score pile. If you do, draw a card of value two higher than the top purple card on your board.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your blue cards right.", Action2)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}