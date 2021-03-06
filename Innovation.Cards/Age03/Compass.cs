﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Compass : CardBase
    {
        public override string Name => "Compass";
        public override int Age => 3;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-green card with a [LEAF] from your board to my board, and then you transfer a top card without a [LEAF] from my board to your board.", Action1)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}