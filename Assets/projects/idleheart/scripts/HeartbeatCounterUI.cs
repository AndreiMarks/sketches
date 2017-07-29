﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartbeatCounterUI : MonoBehaviour 
{
    public Text counter;

    void OnEnable()
    {
        IdleHeartEvents.OnHeartbeatsUpdated += OnHeartbeatsUpdated;
    }

    void OnDisable()
    {
        IdleHeartEvents.OnHeartbeatsUpdated -= OnHeartbeatsUpdated;
    }

    private void OnHeartbeatsUpdated( int heartbeatCount )
    {
        counter.text = heartbeatCount.ToString();
    }
}
