using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public Text textObject;
    
    void OnEnable()
    {
        MicroFarmTimeHandler.OnPeriodTimeUpdated += OnPeriodTimeUpdated;
        MicroFarmTimeHandler.OnPeriodTimeFinished += OnPeriodTimeFinished;

        SetText( "Ready?" );
    }
    
    void OnDisable()
    {
        MicroFarmTimeHandler.OnPeriodTimeUpdated -= OnPeriodTimeUpdated;
        MicroFarmTimeHandler.OnPeriodTimeFinished -= OnPeriodTimeFinished;
    }

    private void SetText( string text )
    {
        textObject.text = text;
    }

    private void OnPeriodTimeUpdated( float time )
    {
        float totalTime = MicroFarmTimeHandler.PERIOD_TIME;
        string timeText = ( totalTime - time ).ToString("f2");
        
        SetText( timeText );
    }

    private void OnPeriodTimeFinished()
    {
        SetText( "Finished" );
    }
}
