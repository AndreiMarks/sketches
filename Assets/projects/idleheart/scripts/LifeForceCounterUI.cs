using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeForceCounterUI : MonoBehaviour 
{
    public Text counter;

    void OnEnable()
    {
        ScoreController.OnLifeForceUpdated += OnLifeForceUpdated;
    }

    void OnDisable()
    {
        ScoreController.OnLifeForceUpdated -= OnLifeForceUpdated;
    }

    private void OnLifeForceUpdated( int amount )
    {
        counter.text = amount.ToString();
    }
}
