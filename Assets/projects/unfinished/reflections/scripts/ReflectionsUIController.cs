using UnityEngine;
using System.Collections;

public class ReflectionsUIController : Controller<ReflectionsUIController>
{
    public UIHandler uiHandler;

    public void ShowMainMenu()
    {
        uiHandler.Activate();
        uiHandler.ShowMainMenuPanel();
    }

    public void ShowChooseItMenu()
    {
        uiHandler.ShowChooseItMenuPanel();
    }
}
