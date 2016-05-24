using Innovation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innovation.Player
{
    public class OrderCards : PlayerAction<IEnumerable<ICard>, string, IEnumerable<ICard>>
    {
        public override Action<string, IEnumerable<ICard>> Handler { get; set; }
    }
}
