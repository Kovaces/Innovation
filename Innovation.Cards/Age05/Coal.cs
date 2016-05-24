using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Coal : CardBase
    {
        public override string Name => "Coal";
        public override int Age => 5;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [5].", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red cards right.", Action2)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may score any one of your top cards. If you do, also score the card beneath it.", Action3)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action3(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}