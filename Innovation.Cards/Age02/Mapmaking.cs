using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Mapmaking : ICard
    {
        public string Name { get { return "Mapmaking"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
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
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			Player activePlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer, out activePlayer);

			List<ICard> cardsToTransfer = targetPlayer.Tableau.ScorePile.Where(x => x.Age == 1).ToList();
			if (cardsToTransfer.Count > 0)
			{
				ICard card = targetPlayer.PickFromMultipleCards(cardsToTransfer, 1, 1).First();
				targetPlayer.Tableau.ScorePile.Remove(card);
				activePlayer.Tableau.ScorePile.Add(card);

				game.StashPropertyBagValue("MapmakingAction1Taken", "true");

				return true;
			}
			return false;
		}
		bool Action2(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			if (game.GetPropertyBagValue("MapmakingAction1Taken").ToString() == "true")
			{
				Score.Action(Draw.Action(1, game), targetPlayer);
				return true;
			}

			return false;
		}
    }
}