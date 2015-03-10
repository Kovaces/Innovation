using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class MachineTools : CardBase
    {
        public override string Name { get { return "Machine Tools"; } }
        public override int Age { get { return 6; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Factory; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a card of value equal to the highest card in your score pile.", Action1)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}