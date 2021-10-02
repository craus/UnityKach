using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class SceneExtensions
{
    public static IEnumerable<T> GetComponents<T>(this Scene scene, bool includeInactive = false)
    {
        return scene.GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<T>(includeInactive: includeInactive));
    }
}