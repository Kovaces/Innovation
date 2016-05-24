using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class MachineTools : CardBase
    {
        public override string Name => "Machine Tools";
        public override int Age => 6;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a card of value equal to the highest card in your score pile.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}