using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.XR;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class MonoBehaviourExtensions
{
    [ContextMenu("WhoGonnaCallYou")]
    public static void WhoGonnaCallYou(this MonoBehaviour mb) {
        Debug.Log("WhoGonnaCallYou");
    }
}