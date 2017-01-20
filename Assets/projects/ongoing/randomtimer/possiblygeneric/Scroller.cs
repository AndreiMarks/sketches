using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scroller : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
	IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
	IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public AudioSource audio;
	public AudioClip valueChangeSound;
	
	public InputField inputField;

	public float minValue = 0f;
	public float maxValue = 60f;

	public float dragModifier = 0.01f;
	
	public float inertiaDuration = 0.95f;

	private float _scrollPosition;
	private float _scrollVelocity;

	private bool _isDragging;
	private float _lastDragTime;

	private int _lastInt;
	
	void Update()
	{
		if (_isDragging) return;
        if ( _scrollVelocity != 0.0f ) 
        {
            // slow down over time
            float t = (Time.time - _lastDragTime) / inertiaDuration;
	        float frameVelocity = Mathf.Lerp(_scrollVelocity, 0f, t);

	        /*
	        Math.easeOutQuad = function (t, b, c, d) {
		        t /= d;
		        return -c * t*(t-2) + b;
	        };
	        */
	        float t1 = Time.time - _lastDragTime;
	        float d = inertiaDuration;
	        float b = _scrollVelocity;
	        float c = -_scrollVelocity;

	        // Quadratic ease out
	        //t1 /= d;
	        //frameVelocity = -c * t1 * (t1 - 2) + b;
	        
            // Exponential Ease Out
	       	frameVelocity = c * (-Mathf.Pow(2f, -10f * t1/d) + 1) + b;

		        
            _scrollPosition += (frameVelocity * Time.deltaTime);

	        _scrollPosition = ClampScrollPosition(_scrollPosition);
            
            // We finish after the time is up.
            if (t >= inertiaDuration) _scrollVelocity = 0.0f;
            
            //SpringToEdge();
            //this.gameObject.transform.position = scrollPosition;
	        SetText(TransformPositionToInt(_scrollPosition).ToString());
        }
	}

	private float ClampScrollPosition(float position)
	{
		if (position < minValue) return maxValue;
		if (position > maxValue) return minValue;
		return position;
	}
	private void ReportValueChanged()
	{
		audio.PlayOneShot(valueChangeSound);		
	}

	private int TransformPositionToInt(float position)
	{
		int newInt = Mathf.RoundToInt(position);
		if (_lastInt != newInt)
		{
			ReportValueChanged();
		}
		_lastInt = newInt;
		return newInt;
	}

	private void SetText(string text)
	{
		inputField.text = text;
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		SetText("0");
		_scrollVelocity = 0f;
		_isDragging = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		float drag;
		float velocity;
		float deltaTime = Time.time - _lastDragTime;
		float deltaPos = eventData.delta.x * dragModifier;
		drag = _scrollPosition;
		
		// Add scroll resistance bit.
		drag += deltaPos; 
        _scrollPosition = drag;
        _scrollPosition = ClampScrollPosition(_scrollPosition);
		
		velocity = deltaPos / deltaTime;
        _scrollVelocity = 0.8f * velocity + 0.2f * _scrollVelocity;

		SetText(TransformPositionToInt(_scrollPosition).ToString());
		_lastDragTime = Time.time;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_isDragging = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Mouse Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse Exit");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Mouse Up");
	}
}