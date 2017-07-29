using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : ReflectionsBehaviour 
{
    // Panels
    public GameObject mainMenuPanelObject;
    public GameObject navigationPanelObject;
    public GameObject chooseItPanelObject;

    public EntryPanel entryPanel;
    public GameObject settingsPanel;

    public Text idText;
    public InputField contentText;

    private ReflectionEntry _currentReflection;

    void Start()
    {
        _Reflections.OnReflectionAccessed += OnReflectionAccessed;
    }

    void OnDestroy()
    {
        _Reflections.OnReflectionAccessed -= OnReflectionAccessed;
    }

    public void Activate()
    {
        Debug.Log("Activating UIHandler.");
        gameObject.SetActive( true );
    }

    #region Panels ==================================================
    private void HideAllPanels()
    {
        mainMenuPanelObject.SetActive( false );
        navigationPanelObject.SetActive( false );
        chooseItPanelObject.SetActive( false );
    }

    public void ShowMainMenuPanel()
    {
        HideAllPanels();
        mainMenuPanelObject.SetActive( true );
    }

    private void ShowNavigationPanel()
    {
        navigationPanelObject.SetActive( true );
    }

    public void ShowChooseItMenuPanel()
    {
        HideAllPanels();
        ShowNavigationPanel();
        chooseItPanelObject.SetActive( true );
    }
    #endregion

    private void OnReflectionAccessed( ReflectionEntry reflection )
    {
        idText.text = reflection.id.ToString();
        contentText.text = reflection.content;

        _currentReflection = reflection;
    }

    public void OnSaveButtonClicked()
    {
        if ( _currentReflection == null ) return;
        _currentReflection.content = contentText.text;
        _Reflections.SaveText();
    }

    public void OnMainLeftButtonClicked()
    {
        _Reflections.MoveFocusDown(); 
    }

    public void OnMainRightButtonClicked()
    {
        _Reflections.MoveFocusUp(); 
    }

    public void ToggleEntryPanel()
    {
        entryPanel.Toggle();
    }

    public void OnSettingsButtonPressed()
    {
        settingsPanel.SetActive( !settingsPanel.activeInHierarchy );
    }
}
