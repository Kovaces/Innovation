using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Reformation : CardBase
    {
        public override string Name => "Reformation";
        public override int Age => 4;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Leaf,"You may tuck a card from your hand for every two [LEAF] on your board.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may splay your yellow or purple cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var leafCount = parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Leaf);

            if (leafCount < 2)
                return;

            var cardsToTuck = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                            new PickCardParameters
                                                                            {
                                                                                CardsToPickFrom = parameters.TargetPlayer.Hand,
                                                                                MinimumCardsToPick = 0,
                                                                                MaximumCardsToPick = (leafCount / 2)
                                                                            }).ToList();

            if (!cardsToTuck.Any())
                return;

            PlayerActed(parameters);

            foreach (var card in cardsToTuck)
            {
                parameters.TargetPlayer.TuckCard(card);
            }
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //You may splay your blue cards right.
            var color = parameters.TargetPlayer.Interaction.PickColor(parameters.TargetPlayer.Id, new List<Color> {Color.Purple, Color.Yellow, Color.None});

            if (color == Color.None)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.SplayStack(color, SplayDirection.Right);
        }
    }
}