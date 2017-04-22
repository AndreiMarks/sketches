using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaximinerButtonPanelButton : ButtonPanelButton
{
    public Image background;
    public Color baseColor;

    public override void SetName( string name )
    {
        textComp.text = name;
        background.color = HSBColor.ShiftHue( baseColor, Random.Range( -1f, 1f ) );
    }

    public void MakeInvisible()
    {
        background.enabled = false;
    }
}
