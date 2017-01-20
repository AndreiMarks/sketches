using UnityEngine;
using System.Collections;

public class App : MonoBehaviour 
{
    [SerializeField] private MorseDisplay _morseDisplay;

    void Start()
    {
        Subscribe();
    }

    void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        MorseListener.OnInputStarted += OnInputStarted;
        MorseListener.OnInputStopped += OnInputStopped;
    }

    private void Unsubscribe()
    {
        MorseListener.OnInputStarted -= OnInputStarted;
        MorseListener.OnInputStopped -= OnInputStopped;
    }

    private void OnInputStarted()
    {
        _morseDisplay.StartDisplay();
    }

    private void OnInputStopped()
    {
        _morseDisplay.StopDisplay();
    }
}
