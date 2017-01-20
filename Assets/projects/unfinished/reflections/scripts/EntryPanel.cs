using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31.ZestKit;

public class EntryPanel : MonoBehaviour 
{
    public RectTransform rectTransform;
    public float duration;
    public EaseType easeType;

    public Vector2 openPosition;
    public Vector2 closedPosition;

    public Text toggledText;

    private bool _isOpen = false;
    private ITween<Vector2> _openTween;

    public bool Toggle()
    {
        _isOpen = !_isOpen;
        Vector2 togglePosition = ( _isOpen ) ? openPosition : closedPosition;
        
        if ( _openTween != null ) _openTween.stop();
        _openTween = rectTransform.ZKanchoredPositionTo( togglePosition, duration ).setEaseType( easeType );
        _openTween.start();

        toggledText.text = ( _isOpen ) ? "CLOSE" : "OPEN";

        return _isOpen;
    }
}
