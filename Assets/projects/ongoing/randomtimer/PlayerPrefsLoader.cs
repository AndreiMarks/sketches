using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsLoader : MonoBehaviour
{
    private const string _MIN_KEY = "LastMin";
    private const string _MAX_KEY = "LastMax";
    
    public Vector2 LoadLastMinMaxSettings()
    {
        float x = PlayerPrefs.GetFloat(_MIN_KEY, 0f);
        float y = PlayerPrefs.GetFloat(_MAX_KEY, 0f);

        return new Vector2(x, y);
    }
    
    public void SaveLastMinMaxSettings(float min, float max)
    {
        PlayerPrefs.SetFloat(_MIN_KEY, min);
        PlayerPrefs.SetFloat(_MAX_KEY, max);
    }
}
