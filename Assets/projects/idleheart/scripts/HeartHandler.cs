using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHandler : IdleHeartBehaviour 
{
    [Header( "Audio" )]
    public AudioSource heartAudio;
    public AudioClip heartBeat;
    public AudioClip heartBeatLubb;
    public AudioClip heartBeatDubb;
    public AudioClip deathBell;
    
    [Header( "Display" )]
    [SerializeField]
    private Transform _display;
    [SerializeField]
    private HeartMeter _heartMeter;

    private bool _isInteracting;
    private float _baseTime = 1f;
    private float _heartTimer;

    // Death ----
    [Header( "Death" )]
    [SerializeField] 
    private float _deathTime;
    [SerializeField]
    private float _deathTimer;

    // Score ----
    private int _heartbeats;
    private int _lifeForce;

    public int CurrentScore { get { return _heartbeats; } }
    public bool IsAlive { get { return _deathTimer > 0f; } }

    public void ResetHeart()
    {
        _heartbeats = 0;
        _lifeForce = 0;

        _events.ReportHeartbeatsUpdated( _heartbeats );
        _events.ReportLifeForceUpdated( _lifeForce );

        ResetDeathTimer();
    }

    public void UpdateHeart()
    {
        RestoreScale();

        if ( !_isInteracting )
        {
            _heartTimer += Time.deltaTime;
            if ( _lifeForce > 0 && 
                  _heartTimer > _baseTime )
            {
                DoAutomaticHeartbeat();
                ResetAutomaticHeartbeat();
            }
        }

        HandleDeathTimer();
    }

    private void AddToLifeForce( int amountToAdd )
    {
        _lifeForce += amountToAdd;
        _events.ReportLifeForceUpdated( _lifeForce );
    }

    private void RemoveFromLifeForce( int amountToRemove )
    {
        _lifeForce -= amountToRemove;
        _events.ReportLifeForceUpdated( _lifeForce );
    }

    private void AddToHeartbeats( int amountToAdd )
    {
        _heartbeats += amountToAdd;
        _events.ReportHeartbeatsUpdated( _heartbeats );

        ResetDeathTimer();
    }

    private void HandleDeathTimer()
    {
        _deathTimer -= Time.deltaTime;
        float deathRatio = _deathTimer / _deathTime;
        _heartMeter.UpdateMeter( deathRatio );
        _events.ReportDeathTimerUpdated( deathRatio );

        if ( _deathTimer <= 0f )
        {
            heartAudio.PlayOneShot( deathBell, .15f );
        }
    }

    private void ResetDeathTimer()
    {
        _deathTimer = _deathTime;
    }

    private void DoAutomaticHeartbeat()
    {
        DoLubbDubb();
        AddToHeartbeats( 1 );
        RemoveFromLifeForce( 1 );
    }

    private void ResetAutomaticHeartbeat()
    {
        _heartTimer = 0f;
    }

    private void PlayerInteractionStarted()
    {
        DoLubb();
        ResetAutomaticHeartbeat();
        _isInteracting = true;
    }

    private void PlayerInteractionComplete()
    {
        DoDubb();
        AddToHeartbeats( 1 );
        AddToLifeForce( 1 );
        _isInteracting = false;
    }

    private void RestoreScale()
    {
        float lerpSpeed = 10f;
        _display.localScale = Vector3.Lerp( _display.localScale, Vector3.one, lerpSpeed * Time.deltaTime );
    }

    private void DoLubb()
    {
        float lubbSize = .75f;
        _display.localScale = lubbSize * Vector3.one; 

    }

    private void DoDubb()
    {
        float dubbSize = 1.35f;
        _display.localScale = dubbSize * Vector3.one; 

        heartAudio.PlayOneShot( heartBeat );
    }

    private void DoLubbDubb()
    {
        float dubbSize = 1.35f;
        _display.localScale = dubbSize * Vector3.one; 

        heartAudio.PlayOneShot( heartBeat );
    }

    void OnMouseDown()
    {
        PlayerInteractionStarted();
    }

    void OnMouseUp()
    {
        PlayerInteractionComplete();
    }

    // Debug
    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) ) _lifeForce = 3;
        if ( Input.GetKeyDown( KeyCode.A ) ) _heartbeats += Random.Range( 100, 1000000);
        if ( Input.GetKeyDown( KeyCode.F ) ) _lifeForce += Random.Range( 100, 1000000);
    }
}
