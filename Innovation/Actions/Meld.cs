using Innovation.Interfaces;


namespace Innovation.Actions
{
    public class Meld
    {
        public static void Action(ICard card, IPlayer player)
        {
            if (card != null)
                player.Tableau.Stacks[card.Color].AddCardToTop(card);
        }
    }
}
