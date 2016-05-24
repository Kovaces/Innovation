using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Databases : CardBase
    {
        public override string Name => "Databases";
        public override int Age => 10;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Clock;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Clock,"I demand you return half (rounded up) of the cards in your score pile!", Action1)
        };

        bool Action1(CardActionParameters parameters)
	    {
		    ValidateParameters(parameters);

		    var selectedCards = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Tableau.ScorePile, (int) Math.Ceiling(parameters.TargetPlayer.Tableau.ScorePile.Count/2.0d), (int) Math.Ceiling(parameters.TargetPlayer.Tableau.ScorePile.Count/2.0d)).ToList();
		    selectedCards.ForEach(c => Return.Action(c, parameters.Game));
			
			return false;
	    }
    }
}