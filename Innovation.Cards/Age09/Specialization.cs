using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Specialization : CardBase
    {
        public override string Name => "Specialization";
        public override int Age => 9;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Reveal a card from your hand. Take into your hand the top card of that color from all other players' boards.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your yellow or blue cards up.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}