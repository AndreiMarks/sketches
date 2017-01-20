using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class MaximinerMapController : Controller<MaximinerMapController>
{
    public MaximinerButtonPanel buttonPanel;
    public MapLocationInfo[] mapLocations;
    
    private MapLocationInfo _currentMapLocation;
    private List<MapObject> _currentMapObjects = new List<MapObject>();

    public void Initialize()
    {
        SetCurrentMapLocation( "TutorialStart" );
        SetCurrentButtons();
    }

    public void AsteroidSelected()
    {
        Debug.Log("Hello.");
    }

    public void MoveToMapLocation( MapLocationInfo mli )
    {
        SetCurrentMapLocation( mli );
        SetCurrentButtons();
    }

    public void SetCurrentMapLocation( string location )
    {
        SetCurrentMapLocation( GetMapLocationByName( location ) );
    }

    public void SetCurrentMapLocation( MapLocationInfo location )
    {
        _currentMapLocation = location;
        buttonPanel.SetMapButtonLayout( _currentMapLocation.layout );
    }

    public void SetCurrentButtons()
    {
        buttonPanel.ClearButtonObjects();

        _currentMapObjects = new List<MapObject>();         

        foreach( MapLocationInfo mli in _currentMapLocation.children )
        {
            MapObject mo = new MapObject( mli );
            ButtonPanelButton button = buttonPanel.AddButtonToPanel( mli.NameForKey, mo.DoThis );
            mo.SetButton( (MaximinerButtonPanelButton)button );

            _currentMapObjects.Add( mo );
        }
    }

    public MapLocationInfo GetMapLocationByName( string locationName )
    {
        return mapLocations.FirstOrDefault( loc => loc.locationName == locationName );
    }
}

public class MapObject
{
    public MapLocationInfo mapLocationInfo;

    private MaximinerButtonPanelButton _button;
    
    public MapObject( MapLocationInfo mli )
    {
        this.mapLocationInfo = mli;
    }

    public void SetButton( MaximinerButtonPanelButton button )
    {
        _button = button;
        if ( string.IsNullOrEmpty( mapLocationInfo.buttonName ) )
        {
            _button.MakeInvisible();
            _button.SetName( string.Empty );
        } else {
            _button.SetName( mapLocationInfo.buttonName );
        }
    }

    public void DoThis()
    {
        Debug.Log("mapLocationInfo: " + mapLocationInfo.buttonName);
        if ( mapLocationInfo.children.Length == 0 ) return;
        MaximinerMapController.Instance.MoveToMapLocation( mapLocationInfo );
    }
}

[System.Serializable]
public class MapLocationInfo
{
    public string locationName;
    public string nameForKey;
    public string buttonName;

    public bool isRoot;
    
    public MapButtonLayout layout;

    public string NameForKey
    {
        get
        {
            if ( string.IsNullOrEmpty( nameForKey ) ) return Random.value.ToString();
            return nameForKey;
        }
    }

    public MapLocationInfo[] children;
}

public enum MapButtonLayout
{
    TwoByEight = 0,
    OneByFive = 1
}
