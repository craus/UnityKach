using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class PauseOnActive : MonoBehaviour {
    public void Awake()
    {
        #if UNITY_EDITOR
        EditorApplication.isPaused = true;
        #endif
    }
}
