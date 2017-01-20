using UnityEngine;
using System.Collections;
using Prime31.ZestKit;

public class CameraMover : ReflectionsBehaviour 
{
    public Vector3 moveSpeeds;

    // Rotation
    public float rotateSpeed = 5f;
    public float maxMagnitudeDelta = 0f;

    private ReflectionCube _focusCube;
    private Vector3 _initialOffset;

    private ITween<UnityEngine.Vector3> _currentTween;

    // Index for focus
    public EaseType focusEaseType;
    
    private int _currentIndex = 0;
    private int _MaxIndex
    {
        get
        {
            return _Reflections.ReflectionCount;
        }
    }

    void OnEnable()
    {
        _Reflections.OnReflectionAccessed += OnReflectionAccessed;
    }

    void OnDisable()
    {
        if ( _Reflections == null ) return;
        _Reflections.OnReflectionAccessed -= OnReflectionAccessed;
    }
	
    void Start ()
    {
        _initialOffset = transform.position;
    }

	void Update () 
    {
        //MoveCameraPosition();	
	}

    void LateUpdate()
    {
        //MoveCameraLook();
    }
    
    private void ChangeFocusTarget( ReflectionCube cube )
    {
        Transform focusTarget = cube.transform;

        float focusDuration = .15f;
        
        // Move the camera to the focus position;
        if ( _currentTween != null ) _currentTween.stop();
        _currentTween = transform.ZKpositionTo( focusTarget.position + _initialOffset, focusDuration )
                                    .setRecycleTween( false )
                                    .setEaseType( focusEaseType );
        _currentTween.start();

        if ( _focusCube != null ) _focusCube.TweenSmall();
        cube.TweenLarge();

        _focusCube = cube;
    }

    void MoveCameraLook()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;

        if ( Physics.Raycast( ray, out hit ) && Input.GetMouseButtonDown( 0 ) )
        {
            ReflectionCube cube = hit.transform.GetComponent<ReflectionCube>();
            if ( cube != null ) _focusCube = cube;
        }

        if ( _focusCube == null ) return;

        Vector3 rotateDirection = ( _focusCube.transform.position - transform.position ).normalized;
        Vector3 rotateVector = Vector3.RotateTowards( transform.forward, rotateDirection, rotateSpeed * Time.deltaTime, maxMagnitudeDelta );
        transform.rotation = Quaternion.LookRotation( rotateVector );
    }

    void MoveCameraPosition()
    {
        float horizontal = Input.GetAxis( "Horizontal" );
        float vertical = Input.GetAxis( "Vertical" );
        float forward = Input.GetAxis( "Vertical" );

        float moveVectorX = horizontal * moveSpeeds.x;
        float moveVectorY = 0f; //vertical * moveSpeeds.y;
        float moveVectorZ = forward * moveSpeeds.z;

        Vector3 moveVector = transform.forward * moveVectorZ + transform.up * moveVectorY + transform.right * moveVectorX;

        transform.localPosition += moveVector * Time.deltaTime;
    }

    void OnReflectionAccessed( ReflectionEntry reflection )
    {
        ChangeFocusTarget( reflection.ReflectionCube );
    }
}
