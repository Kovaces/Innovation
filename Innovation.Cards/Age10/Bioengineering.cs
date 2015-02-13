using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Bioengineering : CardBase
    {
        public override string Name { get { return "Bioengineering"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Lightbulb; } }
        public override Symbol Left { get { return Symbol.Clock; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Transfer a top card with a [LEAF] from any other player's board to your score pile.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"If any player has fewer than three [LEAF] on their board, the single player with the most [LEAF] on their board wins.", Action2)
                };
            }
        }
        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}