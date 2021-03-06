﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Engineering : CardBase
    {
        public override string Name => "Engineering";
        public override int Age => 3;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Tower;
        public override Symbol Left => Symbol.Blank;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer all top cards with a [TOWER] from your board to my score pile!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Tower,"You may splay your red cards left.", Action2)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}