using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.XR;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnityEventListener
{
    public UnityEngine.Object target;
    public string methodName;

    public Component component => target as Component;

    public UnityEventListener(UnityEngine.Object target, string methodName)
    {
        this.target = target;
        this.methodName = methodName;
    }

    public override string ToString()
    {
        return $"target: `{target.ExtToString()}`, method: `{methodName}`";
    }
}