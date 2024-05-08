using UnityEngine;
using TMPro;

public class TextFilterSize : MonoBehaviour
{
    public TMP_Text text;
    public RectTransform p;
    public float offset;
    public void UpdateWidthBackground()
    {
        p.sizeDelta = new Vector2(text.preferredWidth + offset,p.sizeDelta.y);
    }  
}
