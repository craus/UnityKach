using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Endo;
using Common;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class MarkExtensions
{
#if UNITY_EDITOR
    [MenuItem("Utilities/Marks/Find Mark %#m")]
    public static void FindMark()
    {
        Debug.LogFormat("Find Mark");
        if (Selection.objects.Count() == 0)
        {
            Debug.LogFormat("No objects selected");
        }
        Debug.LogFormat("Selection.objects[0] = {0}", Selection.objects[0]);

        if (Selection.objects[0] is Mark)
        {
            Selection.objects = SceneManager.GetActiveScene().GetComponents<Transform>(includeInactive: true).Where(t => t.Mentioned(Selection.objects[0] as Mark)).Select(t => t.gameObject).ToArray();

            if (Selection.objects.Length > 0)
            {
                EditorGUIUtility.PingObject(Selection.objects[0]);
            }
            DebugManager.LogFormat("Selection.objects = {0}", Selection.objects.ExtToString());
        }
    }
#endif
}
