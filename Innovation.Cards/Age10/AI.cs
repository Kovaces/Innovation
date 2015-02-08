﻿using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
    public class AI : ICard
    {
        public string Name { get { return "A.I."; } }
        public int Age { get { return 10; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and score a [10].", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If Robotics and Software are top cards on any board, the single player with the lowest score wins.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}