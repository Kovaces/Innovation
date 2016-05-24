using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Industrialization : CardBase
    {
        public override string Name => "Industrialization";
        public override int Age => 6;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [6] for every two [FACTORY] on your board.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red or purple cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}