using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : Controller<CanvasController>
{
    public GameObject titleScreenObject;
    public GameObject gameScreenObject;
    public GameObject mapScreenObject;
    
    private List<GameObject> _allScreens;
    private List<GameObject> AllScreens
    {
        get
        {
            if ( _allScreens == null )
            {
                _allScreens = new List<GameObject>(){   
                                                        titleScreenObject, 
                                                        gameScreenObject,
                                                    };
            }

            return _allScreens;
        }
    }

    public void ShowTitleScreen()
    {
        HideScreens();
        titleScreenObject.SetActive( true );
    }

    public void ShowGameScreen()
    {
        HideScreens();
        gameScreenObject.SetActive( true );
    }

    private void HideScreens()
    {
        for( int i = 0; i < AllScreens.Count; i++ ) AllScreens[i].SetActive( false );
    }
}
