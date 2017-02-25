using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MaximinerButtonPanel : ButtonPanel
{
    protected override Dictionary<string, Action> _ButtonDict 
    { 
        get 
        {
            if ( _buttonDict == null )
            {
                _buttonDict = new Dictionary<string, Action>() { 
                                                        //{ "Timer", DoTimer },
                                                        //{ "ChooseIt", DoChooseIt },
                                                        //{ "Reflections", DoReflections },
                                                        };
            }

            return _buttonDict;
        } 
    }
}
