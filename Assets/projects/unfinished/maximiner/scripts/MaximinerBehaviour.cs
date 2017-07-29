using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximinerBehaviour : AmBehaviour 
{
    protected CanvasController _Canvas { get { return CanvasController.Instance; } }
    protected MaximinerMapController _Map { get { return MaximinerMapController.Instance; } }
}
