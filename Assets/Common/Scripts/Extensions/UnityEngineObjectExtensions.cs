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

public static class UnityEngineObjectExtensions
{
    public static string ExtToString(this UnityEngine.Object obj) {
        var component = obj as Component;
        if (component != null)
        {
            return $"{obj} (go = {component.transform.Path()})";
        }
        return obj.ToString();
    }

}