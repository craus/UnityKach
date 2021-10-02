using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.XR;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class SystemTypeExtensions
{
    public static IEnumerable<FieldInfo> GetFieldsRecursive(this Type t, BindingFlags bindingFlags) {
        var fields = t.GetFields(bindingFlags | BindingFlags.DeclaredOnly);
        if (t.BaseType != null)
        {
            return t.BaseType.GetFieldsRecursive(bindingFlags).Concat(fields);
        }
        return fields;
    }
}