using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Oars : CardBase
    {
        public override string Name { get { return "Oars"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Tower; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a card with a [CROWN] from your hand to my score pile! If you do, draw a [1]!", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Tower, "If no cards were transfered due to this demand, draw a [1].", Action2)
                };
            }
        }
        bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 3);

			List<ICard> cardsWithCrowns = TargetPlayer.Hand.Where(x => x.HasSymbol(Symbol.Crown)).ToList();
			if (cardsWithCrowns.Count > 0)
			{
				ICard card = TargetPlayer.PickCard(cardsWithCrowns);
				TargetPlayer.Hand.Remove(card);
				Score.Action(card, CurrentPlayer);

				TargetPlayer.Hand.Add(Draw.Action(1, Game));

				Game.StashPropertyBagValue("OarsAction1Taken", "true");

				return true;
			}

			return false;
		}
		bool Action2(object[] parameters)
		{
			ParseParameters(parameters, 2);

			if (Game.GetPropertyBagValue("OarsAction1Taken").ToString() != "true")
			{
				TargetPlayer.Hand.Add(Draw.Action(1, Game));

				return true;
			}

			return false;
		}
    }
}