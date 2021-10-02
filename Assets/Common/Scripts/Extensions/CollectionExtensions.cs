using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public static class CollectionExtensions
{
    public static IEnumerable<T> Single<T>(this T element)
    {
        yield return element;
    }

    public static IEnumerable<T> Empty<T>() {
        yield break;
    }

    public static IEnumerable<T> Unique<T>(this IEnumerable<T> collection) {
        return new HashSet<T>(collection).ToList();
    }

    public static int IndexOfMin<T>(this IList<T> list, Func<T, float> criteria)
    {
        int answer = 0;
        for (int i = 0; i < list.Count(); i++)
        {
            if (criteria(list[i]) < criteria(list[answer]))
            {
                answer = i;
            }
        }
        return answer;
    }

    // https://stackoverflow.com/questions/13054281/listt-foreach-with-index
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<int, T> action)
    {
        // argument null checking omitted
        int i = 0;
        foreach (T item in sequence)
        {
            action(i, item);
            i++;
        }
    }

    public static bool IsIncreasingSequence(this IEnumerable<int> sequence)
    {
        var initiated = false;
        int current = 0; // assigned value never used
        foreach (var element in sequence)
        {
            if (initiated && element <= current)
            {
                return false;
            }
            current = element;
            initiated = true;
        }
        return true;
    }

    //the next two methods are from https://stackoverflow.com/questions/5604168/calculating-count-for-ienumerable-non-generic
    public static void DynamicUsing(object resource, Action action)
    {
        try
        {
            action();
        }
        finally
        {
            IDisposable d = resource as IDisposable;
            if (d != null)
                d.Dispose();
        }
    }
    public static int Count(this IEnumerable source)
    {
        var col = source as ICollection;
        if (col != null)
            return col.Count;

        int c = 0;
        var e = source.GetEnumerator();
        DynamicUsing(e, () =>
        {
            while (e.MoveNext())
                c++;
        });

        return c;
    }

    public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] excludedElements) {
        return Enumerable.Except(source, excludedElements);
    }
}