﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Gunpowder : ICard
    {
        public string Name { get { return "Gunpowder"; } }
        public int Age { get { return 4; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a top card with a [TOWER] from your board to my score pile!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"If any card was transferred due to the demand, draw and score a [2].", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}