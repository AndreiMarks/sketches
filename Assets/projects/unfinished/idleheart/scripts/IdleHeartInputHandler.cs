using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHeartInputHandler : MonoBehaviour 
{
    public bool GetAnyInput()
    {
        return Input.anyKeyDown;
    }
}
