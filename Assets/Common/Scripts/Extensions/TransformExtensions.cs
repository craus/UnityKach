using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class TransformExtensions
{
    public static void Clear(this Transform transform) {
        transform.Children().ForEach(c => UnityEngine.Object.Destroy(c.gameObject));
    }

    public static void MoveASoThatBMatchC(this Transform a, Transform b, Transform c) {
        Transform aParent = a.parent;
        Transform bParent = b.parent;

        b.parent = null;
        a.parent = b;

        b.position = c.transform.position;
        b.rotation = c.transform.rotation;

        a.parent = aParent;
        b.parent = bParent;
    }
}