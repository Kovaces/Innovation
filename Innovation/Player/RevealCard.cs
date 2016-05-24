using System;
using Innovation.Interfaces;


namespace Innovation.Player
{
    public class RevealCard : PlayerAction<bool?, string, ICard>
    {
        public override Action<string, ICard> Handler { get; set; }
    }
}
