using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.XR;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class IEnumerableExtensions
{
    public static bool Any(this IEnumerable ienumerable, Func<object, bool> predicate)
    {
        foreach (object o in ienumerable)
        {
            if (predicate(o))
            {
                return true;
            }
        }
        return false;
    }

    public static IEnumerable<T> Last<T>(this IEnumerable<T> source, int n) {
        return source.Skip(Math.Max(0, source.Count() - n));
    }
}