using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Controller<ScoreController>
{
    public static event Action<int> OnHeartbeatsUpdated = (beats) => {};
    public void ReportHeartbeatsUpdated( int beats )
    {
        OnHeartbeatsUpdated( beats );
    }

    public static event Action<int> OnLifeForceUpdated = (amount) => {};
    public void ReportLifeForceUpdated( int amount )
    {
        OnLifeForceUpdated( amount );
    }

    public static event Action<float> OnDeathTimerUpdated = (ratio) => {};
    public void ReportDeathTimerUpdated( float ratio )
    {
        OnDeathTimerUpdated( ratio );
    }

    public static event Action<int> OnShowScoreScreen = (score) => {};
    public void ReportShowScoreScreen( int score )
    {
        OnShowScoreScreen( score );
    }

    // Utils
    public string GetLivedStringFromHeartbeatCount( int heartbeatCount )
    {
        int minuteInSeconds = 60;
        int hourInSeconds = minuteInSeconds * 60;
        int dayInSeconds = hourInSeconds * 24;
        int yearInSeconds = dayInSeconds * 365;

        string returnString = "";

        // Years
        int yearsCount = heartbeatCount / yearInSeconds;
        int yearsRemainder = heartbeatCount % yearInSeconds;

        if ( yearsCount > 0 )
            returnString += yearsCount.ToString() + " years, ";

        // Days
        int daysCount = yearsRemainder / dayInSeconds;
        int daysRemainder = yearsRemainder % dayInSeconds;
        if ( daysCount > 0 )
            returnString += daysCount.ToString() + " days, ";
        
        // Hours
        int hoursCount = daysRemainder / hourInSeconds;
        int hoursRemainder = daysRemainder % hourInSeconds;
        if ( hoursCount > 0 )
            returnString += hoursCount.ToString() + " hours, ";

        // Minutes
        int minutesCount = hoursRemainder / minuteInSeconds;
        int minutesRemainder = hoursRemainder % minuteInSeconds;
        if (minutesCount > 0 )
            returnString += minutesCount.ToString() + " minutes, "; 
            
        // Seconds
        returnString += minutesRemainder.ToString() + " seconds.";

        return returnString;
    }
}
