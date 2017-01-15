using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;

public class MorseRecorder : MonoBehaviour 
{
    public Text debugText;

    private string FilePath { get { return Application.persistentDataPath + Path.DirectorySeparatorChar + System.DateTime.Today.ToString( "yyyyMMdd" ); } }

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
        string text = GetTimePrepended( "d," );
        SaveText( text );
    }

    private void OnInputStopped()
    {
        string text = GetTimePrepended( "u," );
        SaveText( text );
    }

    private string GetTimePrepended( string text )
    {
        return text + Time.time.ToString() + "," + DateTime.Now.ToString("HHmmss.fff") + Environment.NewLine;
    }
    
    private void SaveText( string text )
    {
        File.AppendAllText( FilePath, text );
        FileInfo file = new FileInfo( FilePath );
        debugText.text = file.Length.ToString();
    }
}

