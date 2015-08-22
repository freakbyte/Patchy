using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patchy
{
    public static class Maths
    {
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;

            return value;
        }
    }
}
