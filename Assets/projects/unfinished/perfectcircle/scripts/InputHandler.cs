using UnityEngine;

namespace PerfectCircle
{
	public class InputHandler : PCBehaviour
	{
		void OnEnable()
		{
			InputSettings.OnTouchBegan += OnTouchBegan;
			InputSettings.OnTouchEnded += OnTouchEnded;
		}

		void OnDisable()
		{
			InputSettings.OnTouchBegan -= OnTouchBegan;
			InputSettings.OnTouchEnded -= OnTouchEnded;
		}

		void OnTouchBegan( TouchInfo ti )
		{
			_CircleHandler.DrawCircleUnit( ti.GetWorldPoint( _MainCam ) );
		}

		void OnTouchEnded( TouchInfo ti )
		{
			_CircleHandler.StopDrawingCurrentCircle();
		}
	}
}
