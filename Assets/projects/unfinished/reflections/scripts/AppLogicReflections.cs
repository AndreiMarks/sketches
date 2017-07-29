using UnityEngine;
using System.Collections;

public class AppLogicReflections : Controller<AppLogicReflections>
{
    public ChooseItController chooseIt;

    private ReflectionsUIController _UI { get { return ReflectionsUIController.Instance; } }

    void Start()
    {
        Debug.Log( "Starting Reflections." );
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        _UI.ShowMainMenu();
    }

    public void InitializeChooseIt()
    {
        Debug.Log("Initializing ChooseIt");
        _UI.ShowChooseItMenu();
        chooseIt.Initialize();
    }
}
