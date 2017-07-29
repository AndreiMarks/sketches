using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31.ZestKit;

public class DeathFader : IdleHeartBehaviour 
{
    public Image fader;
    public Color colorMin;
    public Color colorMax;
    public float valueMax;
    public float valueMin;

    public Image faderIcon;
    public Color iconColorMin;
    public Color iconColorMax;

    void OnEnable()
    {
        IdleHeartEvents.OnDeathTimerUpdated += OnDeathTimerUpdated;
        IdleHeartEvents.OnShowScoreScreen += OnShowScoreScreen;
    }

    void OnDisable()
    {
        IdleHeartEvents.OnDeathTimerUpdated -= OnDeathTimerUpdated;
        IdleHeartEvents.OnShowScoreScreen -= OnShowScoreScreen;
    }

    void OnDeathTimerUpdated( float ratio )
    {
        if ( !fader.enabled || !faderIcon.enabled ) fader.enabled = faderIcon.enabled = true;

        float remappedRatio = ratio.Remap( valueMin, valueMax, 1f, 0f );
        fader.color = Color.Lerp( colorMin, colorMax, remappedRatio );
        faderIcon.color = Color.Lerp( iconColorMin, iconColorMax, remappedRatio );
    }

    void OnShowScoreScreen( int score )
    {
        float fadeTime = 2f;
        fader.ZKcolorTo( colorMin, fadeTime ).setCompletionHandler( (tween) => fader.enabled = false ).start();
        faderIcon.ZKcolorTo( iconColorMin, fadeTime ).setCompletionHandler( (tween) => faderIcon.enabled = false ).start();
    }
}
