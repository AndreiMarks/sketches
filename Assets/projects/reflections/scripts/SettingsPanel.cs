using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SettingsPanel : ReflectionsBehaviour 
{
    public SettingsPanelButton buttonPrefab;
    public UIHandler uiHandler;

    private static SettingsPanel _instance;
    private Dictionary<string, Action> _buttonDict = new Dictionary<string, Action>() {
                                                        { "Link", OnLink },
                                                        { "Unlink", OnUnlink },
                                                        { "Download", OnDownload },
                                                        { "Sync", OnSync },
                                                    };

	void Start () 
    {
        _instance = this;
        CreateButtons();
	}

    void CreateButtons()
    {
        foreach( KeyValuePair kvp in _buttonDict )
        {

        }
    }
	
    private static void OnLink()
    {
        _instance._Dropbox.TryLink();
    }

    private static void OnUnlink()
    {
        _instance._Dropbox.Unlink();
    }

    private static void OnDownload()
    {
        _instance._Reflections.DownloadText();
    }

    private static void OnSync()
    {
        _instance._Reflections.TryLoadText();
    }
}
