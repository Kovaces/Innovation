﻿using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Navigation : CardBase
    {
        public override string Name => "Navigation";
        public override int Age => 4;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a [2] or [3] from your score pile, if it has any, to my score pile!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}