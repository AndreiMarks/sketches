using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ButtonPanelButton : MonoBehaviour 
{
    public Button button;
    public Text textComp;

    public virtual void SetName( string name )
    {
        textComp.text = name;
    }
    
    public virtual void SetClick( Action onClick )
    {
        button.onClick.AddListener( () => onClick() );
    }
}
