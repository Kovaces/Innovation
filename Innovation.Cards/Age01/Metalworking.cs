using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Metalworking : ICard
    {
        public string Name { get { return "Metalworking"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Tower; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Draw and reveal a [1]. If it has a [TOWER], score it and repeat this dogma effect. Otherwise, keep it.", Action1)
                };
            }
        }
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			bool keepGoing = true;

			while (keepGoing)
			{
				ICard card = Draw.Action(1, game);
				targetPlayer.RevealCard(card);

				if (CardHelper.CardHasSymbol(card, Symbol.Tower))
					Score.Action(card, targetPlayer);
				else
				{
					targetPlayer.Hand.Add(card);
					keepGoing = false;
				}
			}

			return true;
		}
    }
}