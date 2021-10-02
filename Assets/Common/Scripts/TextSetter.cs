using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class TextSetter : MonoBehaviour
{
    public StringValueProvider textProvider;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshPro _text;
    public TMPro.TMP_InputField __text;

    public void Update() {
        if (text != null)
        {
            text.text = textProvider.Value;
        }
        if (_text != null)
        {
            _text.text = textProvider.Value;
        }
        if (__text != null) {
            __text.text = textProvider.Value;
        }
    }
}
