using UnityEngine;
using System;
using System.Collections;
using Prime31.ZestKit;
using Random = UnityEngine.Random;

public class ReflectionCube : ReflectionsBehaviour 
{
    // Data
    public ReflectionEntry Reflection { get { return _reflection; } }
    private ReflectionEntry _reflection;

    // Animation
    public Vector3 clickedSize = new Vector3( 1.5f, 1.5f, 1.5f );
    public float tweenLargeDuration = .25f;
    public float tweenSmallDuration = .5f;
    public EaseType tweenLargeEase;
    public EaseType tweenSmallEase;

    // Tween
    private ITween<Vector3> _scaleTween;

    // Drift
    private float _randomSeed = 0f;
    private float _pingPongValue;
    
    void Awake()
    {
        _randomSeed = Random.value;
    }

    void Update()
    {
        //Drift();
    }

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

        _Reflections.ReportReflectionAccessed( _reflection );
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

    private void Drift()
    {
        float driftWidth = 10f;
        _pingPongValue = Mathf.PingPong( Time.time * _randomSeed * .5f, driftWidth );

        float x = -driftWidth * .5f + _pingPongValue;
        float y = transform.localPosition.y;
        float z = transform.localPosition.z;

        transform.localPosition = new Vector3( x, y, z );
    }
}

