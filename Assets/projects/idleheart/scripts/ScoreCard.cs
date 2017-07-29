using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : IdleHeartBehaviour 
{
    public Text heartbeatText;
    public Text livedText;

    void OnEnable()
    {
        IdleHeartEvents.OnShowScoreScreen += SetScoreCard;
    }

    void OnDisable()
    {
        IdleHeartEvents.OnShowScoreScreen -= SetScoreCard;
    }

    public void SetScoreCard( int heartbeats )
    {
        heartbeatText.text = heartbeats.ToString("N0");
        string livedTextString = _score.GetLivedStringFromHeartbeatCount( heartbeats );
        livedText.text = livedTextString;
    }
}
