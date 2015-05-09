using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using Innovation.Models.Other;

namespace Innovation.Cards
{
	public class AI : CardBase
	{
		public override string Name { get { return "A.I."; } }
		public override int Age { get { return 10; } }
		public override Color Color { get { return Color.Purple; } }
		public override Symbol Top { get { return Symbol.Lightbulb; } }
		public override Symbol Left { get { return Symbol.Lightbulb; } }
		public override Symbol Center { get { return Symbol.Clock; } }
		public override Symbol Right { get { return Symbol.Blank; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and score a [10].", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If Robotics and Software are top cards on any board, the single player with the lowest score wins.", Action2)
                };
			}
		}

		void Action1(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			Score.Action(Draw.Action(10, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}

		void Action2(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			var topCards = parameters.Players.SelectMany(p => p.Tableau.GetTopCards()).ToList();

			if (topCards.Exists(c => c.Name.Equals("Robotics")) && topCards.Exists(c => c.Name.Equals("Software")))
			{
				parameters.AddToStorage(ContextStorage.WinnerKey, parameters.Players.OrderBy(p => p.Tableau.GetScore()).ToList().First());
				throw new EndOfGameException();
			}
		}
	}
}