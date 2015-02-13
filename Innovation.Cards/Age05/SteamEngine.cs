using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class SteamEngine : CardBase
    {
        public override string Name { get { return "Steam Engine"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck two [4], then score your bottom yellow card.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}