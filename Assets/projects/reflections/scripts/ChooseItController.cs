using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31.ZestKit;

public class ChooseItController : Controller<ChooseItController> 
{
    public GameObject chooseItEnvironment;

    public Transform optionTextTransform;
    public DynamicText optionTextComponent;
    public float panScale;
    public int defaultOptionCount = 2;
    private int _optionCount;
    
    private Vector3 _initialPosition;
    private Vector3 _lastPanPosition;
    private float _lastUpdateTime;
    private bool _isPanning;

    public AnimationCurve easeCurve;
    public AudioClip popSound;

    private ITween<Vector3> _scaleTween;
    private float _bounceSizeScale = 2f;
    private Vector3 _BounceSize { get { return Vector3.one * _bounceSizeScale; } }
    private float _tweenDuration = .35f;

    private AudioController _Audio { get { return AudioController.Instance; } }
    private TouchKitController _TK { get { return TouchKitController.Instance; } }

    private bool _OptionTextVisible { get { return optionTextTransform.GetComponent<Renderer>().isVisible; } }
    private Vector3 _OptionTextPosition { get { return optionTextTransform.position; } set { optionTextTransform.position = value; } }
    private Vector2 _OptionTextScreenPos { get { return Camera.main.WorldToScreenPoint( optionTextTransform.position ); } }
    private float _BoundsOffset { get { return optionTextTransform.GetComponent<Renderer>().bounds.size.x; } }
    private float _ScreenWidth
    {
        get
        {
            float xMax = Camera.main.ViewportToWorldPoint( new Vector2( 1f, 0f ) ).x;
            float xMin = Camera.main.ViewportToWorldPoint( new Vector2( 0f, 0f ) ).x;
            return xMax - xMin;
        }
    }

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
        _optionCount--;

        if ( _optionCount < 0 ) _optionCount = 0;

        UpdateOptionText();
    }

    public void IncrementOptionNumber()
    {
        _optionCount++;
        UpdateOptionText();
    }

    public void ExecuteChooseIt()
    {
        int currentMax = _optionCount;
        int chosenNumber = Random.Range( 1, currentMax + 1 );
        UpdateOptionText( chosenNumber.ToString() );
    }

    private void UpdateOptionText()
    {
        string newText = _optionCount.ToString();
        UpdateOptionText( newText );
    }

    public void TweenLarge()
    {
        if ( _scaleTween != null ) _scaleTween.stop();
        _scaleTween = optionTextTransform.ZKlocalScaleTo( _BounceSize, _tweenDuration )
                                        //.setEaseType( EaseType.BackOut )
                                        .setAnimationCurve( easeCurve )
                                        .setRecycleTween( false )
                                        .setCompletionHandler( TweenNormal );
        _scaleTween.start();
    }

    public void TweenNormal( ITween<Vector3> tween )
    {
        if ( _scaleTween != null ) _scaleTween.stop();
        _scaleTween = optionTextTransform.ZKlocalScaleTo( Vector3.one, .15f )
                                .setEaseType( EaseType.SineOut )
                                .setRecycleTween( false );
        _scaleTween.start();
    }
    private void UpdateOptionText( string newText )
    {
        _OptionTextPosition = Vector3.zero;
        optionTextComponent.SetText( newText );
        TweenLarge();
        _Audio.PlaySFX( popSound );
    }

    private void OnPanUpdated( TKPanRecognizer pan )
    {
        //Debug.Log( "Pan updated: " + pan );

        if ( !_isPanning )
        {
            //Debug.Log( "Setting initial position." );
            _initialPosition = optionTextTransform.position;
        }

        optionTextTransform.position += Vector3.right * pan.deltaTranslation.x * panScale * Time.deltaTime;

        if ( !_OptionTextVisible )
        {
            if ( _OptionTextScreenPos.x < 0 )
            {
                optionTextTransform.position += Vector3.right * ( _ScreenWidth + _BoundsOffset ); 

            } else if ( _OptionTextScreenPos.x > 1 ) {
            
                optionTextTransform.position -= Vector3.right * ( _ScreenWidth + _BoundsOffset ); 
            }
        }

        float velocity = ( _OptionTextPosition - _lastPanPosition ).magnitude / (Time.time - _lastUpdateTime);
        Debug.Log( velocity );

        _lastUpdateTime = Time.time;
        _lastPanPosition = _OptionTextPosition;
        _isPanning = true;
    }

    private void OnPanEnded( TKPanRecognizer pan )
    {
        //Debug.Log( "Pan ended: " + pan );
        float velocity = ( _OptionTextPosition - _lastPanPosition ).magnitude / (Time.time - _lastUpdateTime);
        Debug.Log( velocity );


        //optionTextTransform.position = Vector3.zero;
        _isPanning = false;
    }

    private void OnSwipeEnded( TKSwipeRecognizer swipe )
    {
        //Debug.Log( "Swipe velocity: " + swipe.swipeVelocity );
    }
}
