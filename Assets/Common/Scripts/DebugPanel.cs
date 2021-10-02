using TMPro;
using UnityEngine;

public sealed class DebugPanel : Singletone<DebugPanel>
{
    [SerializeField]
    private TextMeshPro text;

    [Multiline]
    [SerializeField] [ReadOnly] string fullLog = "";

    [SerializeField]
    private TextMeshProUGUI textUI;

    [SerializeField] private int maxCharacters = 100500;
    [SerializeField] private int maxLines = 4;

    private void Start()
    {
        if (!Extensions.InUnityEditor())
        {
            if (text)
            {
                text.enabled = false;
            }
            if (textUI)
            {
                textUI.enabled = false;
            }
        }
    }

    public void Message(string text)
    {
        if (this.text != null)
        {
            this.text.text += text + "\n";
            this.text.text = this.text.text.Suffix(maxCharacters);
            this.text.text = this.text.text.LastLines(maxLines);
        }
        if (textUI != null)
        {
            textUI.text += text + "\n";
            textUI.text = textUI.text.Suffix(maxCharacters);
            textUI.text = textUI.text.LastLines(maxLines);
        }
        fullLog += text + "\n";
    }
}
