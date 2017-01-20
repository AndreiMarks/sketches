using UnityEngine;
using System;
using System.Collections;

public class MorseListener : MonoBehaviour 
{
    public static event Action OnInputStarted = delegate {};
    public static event Action OnInputStopped = delegate {};

    public bool HasInputNow { get { return GetInput(); } }

	void Update () 
    {
        ListenForInput();
	}

    private bool GetInput()
    {
        return Input.GetKey( KeyCode.Space );
    }

    private void ListenForInput()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) || Input.GetMouseButtonDown( 0 ) )
        {
            OnInputStarted();
        }

        if ( Input.GetKeyUp( KeyCode.Space ) || Input.GetMouseButtonUp( 0 ) )
        {
            OnInputStopped();
        }
    }
}
