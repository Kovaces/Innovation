using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Mapmaking : CardBase
    {
        public override string Name { get { return "Mapmaking"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Demand, Symbol.Crown, "I demand you transfer a [1] from your score pile, if it has any, to my score pile!", Action1)
                    , new CardAction(ActionType.Required, Symbol.Crown, "If any card was transferred due to the demand, draw and score a [1].", Action2)
                };
            }
        }
		bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<ICard> cardsToTransfer = parameters.TargetPlayer.Tableau.ScorePile.Where(x => x.Age == 1).ToList();
			
			if (cardsToTransfer.Count == 0)
				return false;
			
			ICard card = parameters.TargetPlayer.PickCard(cardsToTransfer);
			parameters.TargetPlayer.Tableau.ScorePile.Remove(card);
			parameters.ActivePlayer.Tableau.ScorePile.Add(card);

			parameters.Game.StashPropertyBagValue("MapmakingAction1Taken", true);
			
			return false;
		}
		bool Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (!(bool)parameters.Game.GetPropertyBagValue("MapmakingAction1Taken"))
				return false;
			
			Score.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);
			
			return true;
		}
    }
}