using UnityEngine;
using System;
using System.Collections;

public class TouchKitController : Controller<TouchKitController> 
{
    public event Action<TKPanRecognizer> panUpdatedEvent = ( pan ) => {};
    public event Action<TKPanRecognizer> panEndedEvent = ( pan ) => {};

    public event Action<TKSwipeRecognizer> swipeEndedEvent = ( swipe ) => {};

    private TKTapRecognizer _tap;
    private TKLongPressRecognizer _longPress;
    private TKPanRecognizer _pan;
    private TKSwipeRecognizer _swipe;

	void Start () 
    {
        //AddTapRecognizer();
        //AddLongPressRecognizer();
        AddPanRecognizer();
        AddSwipeRecognizer();
	}

    private void AddTapRecognizer()
    {
        _tap = new TKTapRecognizer();	

        _tap.gestureRecognizedEvent += ( r ) =>
        {
            Debug.Log( "tap recognizer fired: " + r );
        };
        TouchKit.addGestureRecognizer( _tap );
    }

    private void AddLongPressRecognizer()
    {
        _longPress = new TKLongPressRecognizer();
        _longPress.gestureRecognizedEvent += ( r ) =>
        {
            Debug.Log( "long press recognizer fired: " + r );
        };

        _longPress.gestureCompleteEvent += ( r ) =>
        {
            Debug.Log( "long press recognizer finished: " + r );
        };

        TouchKit.addGestureRecognizer( _longPress );
    }
    
    private void AddPanRecognizer()
    {
        float minPanDistance = .1f;
        _pan = new TKPanRecognizer( minPanDistance );

        // when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
        _pan.gestureRecognizedEvent += ( r ) =>
        {
            //Camera.main.transform.position -= new Vector3( _pan.deltaTranslation.x, _pan.deltaTranslation.y ) / 100;
            //Debug.Log( "pan recognizer fired: " + r );
            panUpdatedEvent( r );
        };

        // continuous gestures have a complete event so that we know when they are done recognizing
        _pan.gestureCompleteEvent += r =>
        {
            //Debug.Log( "pan gesture complete" );
            panEndedEvent( r );
        };
        
        TouchKit.addGestureRecognizer( _pan );
    }

    private void AddSwipeRecognizer()
    {
        _swipe = new TKSwipeRecognizer();
        _swipe.timeToSwipe = 0f;
        _swipe.triggerWhenCriteriaMet = false;
        _swipe.gestureRecognizedEvent += ( r ) =>
        {
            //Debug.Log( "swipe recognizer fired: " + r );
            swipeEndedEvent( r );
        };
        TouchKit.addGestureRecognizer( _swipe );
    }
}
