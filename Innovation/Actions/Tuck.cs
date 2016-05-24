using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Actions
{
    public class Tuck
    {
        public static void Action(ICard card, IPlayer player)
        {
            player.Tableau.Stacks[card.Color].AddCardToBottom(card);
        }
    }
}
