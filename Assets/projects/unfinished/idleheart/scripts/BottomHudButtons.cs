using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomHudButtons : IdleHeartBehaviour 
{
    public void OpenRecords()
    {

    }
    
    public void OpenShop()
    {
        Debug.Log( "Opening body shop." );
        _store.OpenShop();
    }

    public void OpenUpgrades()
    {
        
    }
}
