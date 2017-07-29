using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontPanel : IdleHeartBehaviour 
{
    public void ClosePanel()
    {
        _ui.CloseLastOpenedPanel();
    }
}
