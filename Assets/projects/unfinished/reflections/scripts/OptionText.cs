using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionText : ReflectionsBehaviour 
{
    public float dragScale = 1f;

    private bool _shouldMoveAuto = true;
    private Vector3 _lastPosition;
    private float _targetVelocity;

    private Vector3 _CurrentPosition { get { return transform.position; } }
    private float _CurrentVelocity { get { return ( _CurrentPosition - _lastPosition ).magnitude / Time.deltaTime; } }

    private Vector2 _OptionTextScreenPos { get { return Camera.main.WorldToScreenPoint( transform.position ); } }
    private bool _OptionTextVisible { get { return GetComponent<Renderer>().isVisible; } }

    private float _BoundsOffset { get { return GetComponent<Renderer>().bounds.size.x; } }
    private float _ScreenWidth
    {
        get
        {
            float xMax = Camera.main.ViewportToWorldPoint( new Vector2( 1f, 0f ) ).x;
            float xMin = Camera.main.ViewportToWorldPoint( new Vector2( 0f, 0f ) ).x;
            return xMax - xMin;
        }
    }

	void Update () 
    {
        UpdateMove();
        UpdateVelocity();		
	}

    public void ForceUpdatePosition( float moveAmount, bool immediate = false )
    {
        float timeScale = ( immediate ) ? 1f : Time.deltaTime;
        transform.position += Vector3.right * timeScale * moveAmount;

        CheckOffscreen();
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void SetMoveAuto()
    {
        _shouldMoveAuto = true;
    }

    public void SetMovePlayer()
    {
        _shouldMoveAuto = false;
    }

    private void CheckOffscreen()
    {
        if ( !_OptionTextVisible )
        {
            if ( _OptionTextScreenPos.x < 0 )
            {
                ForceUpdatePosition( _ScreenWidth + _BoundsOffset, immediate: true );
                _ChooseIt.DecrementOptionNumber( resetPosition: false );

            } else if ( _OptionTextScreenPos.x > 1 ) {
            
                ForceUpdatePosition( -( _ScreenWidth + _BoundsOffset ), immediate: true );
                _ChooseIt.IncrementOptionNumber( resetPosition: false );
            }
        }
    }

    private void UpdateMove()
    {
        if ( !_shouldMoveAuto ) return;

        ForceUpdatePosition( _targetVelocity );
    }

    private void UpdateVelocity()
    {
        Debug.Log( _CurrentVelocity );
        if ( _shouldMoveAuto )
        {
            _targetVelocity = Mathf.Lerp( _targetVelocity, 0f, dragScale * Time.deltaTime );
        } else {
            _targetVelocity = _CurrentVelocity;
        }

        _lastPosition = _CurrentPosition;
    }
}
