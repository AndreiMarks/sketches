using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximiner : MaximinerBehaviour 
{
	void Start () 
    {
        StartGame();		
	}
    
    private void StartGame()
    {
        Debug.Log( "Starting Maximiner." );
        _Canvas.ShowTitleScreen();
    }
}
