﻿using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Corporations : CardBase
    {
        public override string Name => "Corporations";
        public override int Age => 8;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a top non-green card with a [FACTORY] from your board to my score pile! If you do, draw and meld an [8]!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and meld an [8].", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}