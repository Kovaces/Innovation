﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Innovation.Extensions
{
    public static class ShuffleExtensions
    {
        public static List<T> Shuffle<T>(this List<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (rng == null) throw new ArgumentNullException(nameof(rng));

            for (var i = 0; i < source.Count(); i++)
            {
                var j = rng.Next(i, source.Count());
                var jElement = source.ElementAt(j);
                
                source[j] = source[i];
                source[i] = jElement;
            }

            return source;
        }
    }
}
