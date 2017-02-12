using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Bones;

public abstract class ButtonPanel : ReflectionsBehaviour 
{
    public ButtonPanelButton buttonPrefab;
    public Transform buttonHolder;

    protected Dictionary<string, Action> _buttonDict;
    protected abstract Dictionary<string, Action> _ButtonDict { get; }

	void Start () 
    {
        CreateButtons();
	}

    void CreateButtons()
    {
        foreach( KeyValuePair<string, Action> kvp in _ButtonDict )
        {
            ButtonPanelButton button = buttonHolder.InstantiateChild( buttonPrefab );
            button.transform.ZeroOut();
            button.SetName( kvp.Key );
            button.SetClick( kvp.Value );
        }
    }
}
