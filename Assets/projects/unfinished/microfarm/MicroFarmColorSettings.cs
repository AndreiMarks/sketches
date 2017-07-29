using UnityEngine;
using System.Linq;

public class MicroFarmColorSettings : Settings<MicroFarmColorSettings>
{
    public GradientInfo[] gradients;

    public Color GetRandomColorByGradientType( ThemeColor type )
    {
        GradientInfo gradient = gradients.FirstOrDefault( t => t.type == type );

        if ( gradient == null ) return Color.black;

        return gradient.GetRandomColor();
    }
}

public enum ThemeColor
{
    BlueSky,
    GreenGround,
    BrownGround,
    YellowSun
}

[System.Serializable]
public class GradientInfo
{
    public ThemeColor type;
    public Color colorOne;
    public Color colorTwo;

    public Color GetRandomColor()
    {
        return Color.Lerp( colorOne, colorTwo, Random.value );
    }
}
