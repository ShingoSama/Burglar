using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatDisplay : MonoBehaviour
{
    public TextMeshProUGUI LabelText;
    public TextMeshProUGUI BaseValueText;
    public TextMeshProUGUI ValueText;
    public void OnValidate()
    {
        TextMeshProUGUI[] text = GetComponentsInChildren<TextMeshProUGUI>();
        LabelText = text[0];
        ValueText = text[1];
    }
}
