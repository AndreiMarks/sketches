using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : ReflectionsBehaviour 
{
    public Text idText;
    public InputField contentText;
    public EntryPanel entryPanel;
    public GameObject settingsPanel;

    private ReflectionEntry _currentReflection;

    void Start()
    {
        _Reflections.OnReflectionAccessed += OnReflectionAccessed;
    }

    void OnDestroy()
    {
        _Reflections.OnReflectionAccessed -= OnReflectionAccessed;
    }

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
