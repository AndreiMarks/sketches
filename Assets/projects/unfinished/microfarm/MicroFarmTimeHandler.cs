using System;
using System.Collections;
using UnityEngine;

public class MicroFarmTimeHandler : MonoBehaviour
{
    public static event Action<float> OnPeriodTimeUpdated = delegate { };
    public static event Action OnPeriodTimeFinished = delegate { };
    
    public const float PERIOD_TIME = 7f;
    
    public IEnumerator StartPeriod()
    {
        Debug.Log( "Starting period." );
        
        float startTime = 0f;
        while ( startTime < PERIOD_TIME )
        {
            startTime += Time.deltaTime;
            OnPeriodTimeUpdated( startTime );
            
            yield return 0;
        }

        StartCoroutine( EndPeriod() );
    }

    public IEnumerator EndPeriod()
    {
        Debug.Log( "Ending period." );
        
        OnPeriodTimeFinished();
        yield return 0;
    }
}
