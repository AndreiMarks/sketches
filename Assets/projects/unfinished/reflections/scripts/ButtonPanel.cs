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
        CreateButtonObjects();
	}

    public void CreateButtonObjects()
    {
        foreach( KeyValuePair<string, Action> kvp in _ButtonDict )
        {
            CreateButtonObject( kvp.Key, kvp.Value );
        }
    }

    public virtual ButtonPanelButton CreateButtonObject( string name, Action onClick )
    {
        ButtonPanelButton button = buttonHolder.InstantiateChild( buttonPrefab );
        button.transform.ZeroOut();
        button.SetName( name );
        button.SetClick( onClick );

        return button;
    }

    public void ClearButtonObjects()
    {
        buttonHolder.DestroyAllChildren();
    }
}
