﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Services : CardBase
    {
        public override string Name => "Services";
        public override int Age => 9;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you transfer all the highest cards from your score pile to my hand! If you transferred any cards, then transfer a top card from my board without a [LEAF] to your hand!", Action1)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}