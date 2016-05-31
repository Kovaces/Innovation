using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Invention : CardBase
    {
        public override string Name => "Invention";
        public override int Age => 4;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay right any one color of your cards currently splayed left. If you do, draw and score a [4].", Action1)
            ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If you have five colors splayed, each in any direction, claim the Wonder achievement.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            //You may splay right any one color of your cards currently splayed left
            var splayableStacks = parameters.TargetPlayer.Tableau.Stacks.Where(s => s.Value.SplayedDirection == SplayDirection.Left).ToList();
            if (splayableStacks.Any())
            {
                var colors = splayableStacks.Select(s => s.Key).ToList();
                colors.Add(Color.None);

                var selectedColor = parameters.TargetPlayer.Interaction.PickColor(parameters.TargetPlayer.Id, colors);
                if (selectedColor == Color.None)
                    return;
                
                PlayerActed(parameters);
                parameters.TargetPlayer.SplayStack(selectedColor, SplayDirection.Right);
                    
                //If you do, draw and score a[4].
                Score.Action(Draw.Action(4, parameters.AgeDecks), parameters.TargetPlayer);
            }

        }

        void Action2(ICardActionParameters parameters)
        {
            //If you have five colors splayed, each in any direction, claim the Wonder achievement.
            if (parameters.TargetPlayer.Tableau.Stacks.Count(s => s.Value.SplayedDirection != SplayDirection.None) == 5)
            {
                // TODO::achieve Wonder.  Special achievements need a larger framework and some discussion
                throw new NotImplementedException("Wonder Achievement"); 
                PlayerActed(parameters);
            }            
        }
    }
}