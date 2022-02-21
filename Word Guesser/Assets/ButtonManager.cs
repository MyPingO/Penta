using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button button;
    public Color32 color;

    public void ChangeButtonColor()
    {
        ColorBlock buttonColor = button.colors;
        buttonColor.normalColor = buttonColor.selectedColor;
        button.colors = buttonColor;
    }
}
