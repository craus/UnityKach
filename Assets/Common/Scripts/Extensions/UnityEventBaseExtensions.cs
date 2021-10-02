using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class UnityEventBaseExtensions
{
    public static IEnumerable<UnityEventListener> Listeners(this UnityEventBase e) {
        for (int i = 0; i < e.GetPersistentEventCount(); i++)
        {
            yield return new UnityEventListener(e.GetPersistentTarget(i), e.GetPersistentMethodName(i));
        }
    }
}