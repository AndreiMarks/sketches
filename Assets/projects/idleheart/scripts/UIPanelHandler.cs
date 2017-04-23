﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class UIPanelHandler : MonoBehaviour 
{
    public PanelInfo[] panelInfos;

    public void HideAllPanels()
    {
        for( int i = 0; i < panelInfos.Length; i++ )
        {
            panelInfos[i].HidePanel(); 
        }
    }

    public void ShowPanelByName( string name )
    {
        PanelInfo pi = panelInfos.FirstOrDefault( panel => panel.panelName == name );        

        if ( pi == null )
        {
            Debug.Log( string.Format( "No Panel called {0} found!", name ) );
            return;
        }

        pi.ShowPanel();
    }

    public void ShowSinglePanelByName( string name )
    {
        HideAllPanels();
        ShowPanelByName( name );
    }
}

[Serializable]
public class PanelInfo
{
    public string panelName;
    public GameObject panelObject;

    public void HidePanel()
    {
        panelObject.SetActive( false );
    }

    public void ShowPanel()
    {
        panelObject.SetActive( true );
    }
}
