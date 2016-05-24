using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
	public class Paper : CardBase
	{
        public override string Name => "Paper";
        public override int Age => 3;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your green or blue cards left.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw a [4] for every color you have splayed left.", Action2)
        };


		void Action1(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var validColors = new List<Color>();

			if (parameters.TargetPlayer.Tableau.Stacks[Color.Green].Cards.Count > 1)
				validColors.Add(Color.Green);

			if (parameters.TargetPlayer.Tableau.Stacks[Color.Blue].Cards.Count > 1)
				validColors.Add(Color.Blue);

			if (!validColors.Any())
				return;
			
			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your green or blue cards left.");
			if (!answer.HasValue || !answer.Value)
				return;

			PlayerActed(parameters);

			var selectedColor = parameters.TargetPlayer.Interaction.PickColor(parameters.TargetPlayer.Id, validColors);

			parameters.TargetPlayer.SplayStack(selectedColor, SplayDirection.Left);
		}

		void Action2(ICardActionParameters parameters)
		{
			ValidateParameters(parameters);

			for (var i = 0; i < parameters.TargetPlayer.Tableau.Stacks.Count(s => s.Value.SplayedDirection == SplayDirection.Left); i++)
			{
				parameters.TargetPlayer.AddCardToHand(Draw.Action(4, parameters.AgeDecks));
			}

			PlayerActed(parameters);
		}
	}
}