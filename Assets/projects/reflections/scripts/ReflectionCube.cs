using UnityEngine;
using System;
using System.Collections;
using Prime31.ZestKit;
using Random = UnityEngine.Random;

public class ReflectionCube : MonoBehaviour 
{
    // Data
    private ReflectionEntry _reflection;

    // Animation
    public Vector3 clickedSize = new Vector3( 1.5f, 1.5f, 1.5f );
    public float tweenLargeDuration = .25f;
    public float tweenSmallDuration = .5f;
    public EaseType tweenLargeEase;
    public EaseType tweenSmallEase;

    // Tween
    private ITween<Vector3> _scaleTween;
    
    void OnMouseDown()
    {
        TweenLarge();
        AccessReflection();
    }

    void OnMouseUp()
    {
        TweenSmall();
    }
    
    public void Initialize( ReflectionEntry entry )
    {
        _reflection = entry;
    }

    public void AccessReflection()
    {
        if ( _reflection == null ) return;

        ReflectionManager.Instance.ReportReflectionAccessed( _reflection );
    }

    public void TweenLarge()
    {
        if ( _scaleTween != null ) _scaleTween.stop();
        _scaleTween = transform.ZKlocalScaleTo( clickedSize, tweenLargeDuration )
                                .setEaseType( tweenLargeEase )
                                .setRecycleTween( false );
        _scaleTween.start();
    }

    public void TweenSmall()
    {
        if ( _scaleTween != null ) _scaleTween.stop();
        _scaleTween = transform.ZKlocalScaleTo( Vector3.one, tweenSmallDuration )
                                .setEaseType( tweenSmallEase )
                                .setRecycleTween( false );
        _scaleTween.start();
    }
}

