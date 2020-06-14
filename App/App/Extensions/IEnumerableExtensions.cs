using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T> (this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new NullReferenceException("Source cannot be null");
            if (action == null) throw new NullReferenceException("Action cannot be null");
            foreach(T element in source)
            {
                action(element);
            }
            return source;
        }
    }
}
