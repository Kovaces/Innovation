using System;

namespace Innovation.Player
{
    public class AskQuestion : PlayerAction<bool?, string, string>
    {
        public override Action<string, string> Handler { get; set; }
    }
}
