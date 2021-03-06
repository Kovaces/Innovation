﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Colonialism : CardBase
    {
        public override string Name => "Colonialism";
        public override int Age => 4;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [3]. If it has a [CROWN], repeat this dogma effect.", Action1)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}