using UnityEngine;
using System.Collections;

public class ReflectionsBehaviour : MonoBehaviour 
{
    private static bool _preWarmed;

    protected DropboxController _Dropbox { get { return DropboxController.Instance; } }
    protected ReflectionController _Reflections { get { return ReflectionController.Instance; } }

    void Awake()
    {
        if ( !_preWarmed )
        {
            Debug.Log( "Loading: " + _Dropbox );
            Debug.Log( "Loading: " + _Reflections );
            _preWarmed = true;
        }
    }
}
