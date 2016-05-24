using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Game;
using Innovation.Interfaces;

using Innovation.Models.Other;
using Innovation.Storage;


namespace Innovation.Cards
{
	public class AI : CardBase
	{
		public override string Name => "A.I.";
	    public override int Age => 10;
	    public override Color Color => Color.Purple;
	    public override Symbol Top => Symbol.Lightbulb;
	    public override Symbol Left => Symbol.Lightbulb;
	    public override Symbol Center => Symbol.Clock;
	    public override Symbol Right => Symbol.Blank;

	    public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
	        new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and score a [10].", Action1)
	        ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If Robotics and Software are top cards on any board, the single player with the lowest score wins.", Action2)
	    };

	    void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			Score.Action(Draw.Action(10, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}

		void Action2(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			var topCards = parameters.Players.SelectMany(p => p.Tableau.GetTopCards()).ToList();

			if (topCards.Exists(c => c.Name.Equals("Robotics")) && topCards.Exists(c => c.Name.Equals("Software")))
			{
                parameters.AddToStorage("WinnerKey", parameters.Players.OrderBy(p => p.Tableau.GetScore()).ToList().First());
				throw new EndOfGameException();
			}
		}
	}
}