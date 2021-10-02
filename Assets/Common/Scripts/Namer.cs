using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Namer : MonoBehaviour
{
    public string text;
    public string format = "{0}";

    public Namer parent;

    public string Text => parent != null ? parent.Text : text;

    public string Name => format.i(Text);

    public void Update()
    {
#if UNITY_EDITOR
        if (name != Name)
        {
            name = Name;
            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}
