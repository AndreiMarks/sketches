using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeForceCounterUI : MonoBehaviour 
{
    public Text counter;

    void OnEnable()
    {
        IdleHeartEvents.OnLifeForceUpdated += OnLifeForceUpdated;
    }

    void OnDisable()
    {
        IdleHeartEvents.OnLifeForceUpdated -= OnLifeForceUpdated;
    }

    private void OnLifeForceUpdated( int amount )
    {
        counter.text = amount.ToString();
    }
}
