using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class Monotheism : CardBase
    {
        public override string Name { get { return "Monotheism"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Tower; } }
        public override Symbol Center { get { return Symbol.Tower; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Tower, "Draw and tuck a [1].", Action2)
                };
            }
        }
        bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 3);

			List<Color> activePlayerTopColors = CurrentPlayer.Tableau.GetStackColors();
			List<ICard> possibleTransferCards = TargetPlayer.Tableau.GetTopCards().Where(x => !activePlayerTopColors.Contains(x.Color)).ToList();

			if (possibleTransferCards.Count > 0)
			{
				ICard cardToTransfer = TargetPlayer.PickCard(possibleTransferCards);
				TargetPlayer.Tableau.Stacks[cardToTransfer.Color].RemoveCard(cardToTransfer);
				CurrentPlayer.Tableau.ScorePile.Add(cardToTransfer);

				Tuck.Action(Draw.Action(1, Game), TargetPlayer);

				return true;
			}

			return false;
		}
        bool Action2(object[] parameters) 
		{
			ParseParameters(parameters, 2);

			Tuck.Action(Draw.Action(1, Game), TargetPlayer);

			return true;
		}
    }
}