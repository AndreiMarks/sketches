using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour 
{
    public Text idText;
    public InputField contentText;

    public ReflectionManager Reflections { get { return ReflectionManager.Instance; } }

    private ReflectionEntry _currentReflection;

    void Start()
    {
        Reflections.OnReflectionAccessed += OnReflectionAccessed;
    }

    void OnDestroy()
    {
        Reflections.OnReflectionAccessed -= OnReflectionAccessed;
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
        Reflections.SaveText();
    }
}
