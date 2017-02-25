using UnityEngine;
using System.Collections;

public class ReflectionsBehaviour : MonoBehaviour 
{
    protected AppLogicReflections _AppLogic { get { return AppLogicReflections.Instance; } }

    protected AudioController _Audio { get { return AudioController.Instance; } }
    protected ChooseItController _ChooseIt { get { return ChooseItController.Instance; } }
    protected DropboxController _Dropbox { get { return DropboxController.Instance; } }
    protected TouchKitController _TouchKit { get { return TouchKitController.Instance; } }
    protected ReflectionController _Reflections { get { return ReflectionController.Instance; } }
    protected ReflectionsUIController _UI { get { return ReflectionsUIController.Instance; } } 
}
