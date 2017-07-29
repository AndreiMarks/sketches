using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31.ZestKit;

public class ChooseItController : Controller<ChooseItController> 
{
    public GameObject chooseItEnvironment;

    public OptionText optionText;
    public DynamicText optionTextComponent;
    public float panScale;
    public int defaultOptionCount = 2;
    private int _optionCount;
    
    private bool _isPanning;

    public AnimationCurve updateCurve;
    public AnimationCurve executeCurve;
    public AudioClip updateSound;
    public AudioClip executeSound;

    private ITween<Vector3> _scaleTween;
    private float _bounceSizeScale = 2f;
    private Vector3 _BounceSize { get { return Vector3.one * _bounceSizeScale; } }
    private float _tweenDuration = .35f;

    private AudioController _Audio { get { return AudioController.Instance; } }
    private TouchKitController _TK { get { return TouchKitController.Instance; } }

    private Transform _OptionTextTransform { get { return optionText.transform; } }

    void OnEnable()
    {
        _TK.panUpdatedEvent += OnPanUpdated;
        _TK.panEndedEvent += OnPanEnded;
        _TK.swipeEndedEvent += OnSwipeEnded;
    }

    void OnDisable()
    {
        _TK.panUpdatedEvent -= OnPanUpdated;
        _TK.panEndedEvent -= OnPanEnded;
        _TK.swipeEndedEvent -= OnSwipeEnded;
    }

    public void Initialize()
    {
        // The point of the Choose it is to provide an interface for picking a number.
        // Then picking randomly based on that number.
        Debug.Log( "I am choose it." );

        chooseItEnvironment.SetActive( true );
    }

    public void DecrementOptionNumber()
    {
        DecrementOptionNumber( resetPosition: true );
    }

    public void DecrementOptionNumber( bool resetPosition = true )
    {
        _optionCount--;

        if ( _optionCount < 0 ) _optionCount = 0;

        UpdateOptionText( resetPosition, audio: updateSound );
    }

    public void IncrementOptionNumber()
    {
        IncrementOptionNumber( resetPosition: true );
    }

    public void IncrementOptionNumber( bool resetPosition = true )
    {
        _optionCount++;
        UpdateOptionText( resetPosition, audio: updateSound );
    }

    public void ExecuteChooseIt()
    {
        int currentMax = _optionCount;
        int chosenNumber = Random.Range( 1, currentMax + 1 );
        UpdateOptionText( chosenNumber.ToString(), resetPosition: true, audio: executeSound );
    }

    private void UpdateOptionText( bool resetPosition, AudioClip audio )
    {
        string newText = _optionCount.ToString();
        UpdateOptionText( newText, resetPosition, updateSound );
    }

    private void UpdateOptionText( string newText, bool resetPosition, AudioClip audio )
    {
        if ( resetPosition ) optionText.ResetPosition();
        optionTextComponent.SetText( newText );
        AnimationCurve curve = ( audio == executeSound ) ? executeCurve : updateCurve;
        Tween( curve );
        _Audio.PlaySFX( audio );
    }

    public void Tween( AnimationCurve curve )
    {
        _OptionTextTransform.localScale = Vector3.one;
        if ( _scaleTween != null ) _scaleTween.stop();
        _scaleTween = _OptionTextTransform.ZKlocalScaleTo( _BounceSize, _tweenDuration )
                                        .setAnimationCurve( curve )
                                        .setRecycleTween( false )
                                        .setCompletionHandler( TweenNormal );
        _scaleTween.start();
    }

    public void TweenNormal( ITween<Vector3> tween )
    {
        if ( _scaleTween != null ) _scaleTween.stop();
        _scaleTween = _OptionTextTransform.ZKlocalScaleTo( Vector3.one, .15f )
                                .setEaseType( EaseType.SineOut )
                                .setRecycleTween( false );
        _scaleTween.start();
    }

    private void OnPanUpdated( TKPanRecognizer pan )
    {
        //Debug.Log( "Pan updated: " + pan );

        if ( !_isPanning )
        {
            //Debug.Log( "Setting initial position." );

            optionText.SetMovePlayer();
        }

        optionText.ForceUpdatePosition( pan.deltaTranslation.x * panScale );
        _isPanning = true;
    }

    private void OnPanEnded( TKPanRecognizer pan )
    {
        _isPanning = false;
        optionText.SetMoveAuto();
    }

    private void OnSwipeEnded( TKSwipeRecognizer swipe )
    {
        //Debug.Log( "Swipe velocity: " + swipe.swipeVelocity );
    }
}
