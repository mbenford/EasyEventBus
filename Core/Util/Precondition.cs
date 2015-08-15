using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyEventBus.Util
{
    internal static class Precondition
    {
        public static void NotNull<T>(T value) where T : class
        {
            if (value == null) throw new ArgumentNullException();
        }

        public static void NotEmpty<T>(IEnumerable<T> value)
        {
            NotNull(value);
            if (!value.Any()) throw new ArgumentException("Argument cannot be empty");
        }
    }
}
