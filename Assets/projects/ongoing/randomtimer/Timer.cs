using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RandomTimer
{
	public class Timer : MonoBehaviour
	{
		public Text textField;
		private IEnumerator _countdownTimer;
		
		public void StartTimer( float runtime )
		{
			Debug.Log( "Running timer with " + runtime + " seconds." );
			if ( _countdownTimer != null )
			{
				StopCoroutine( _countdownTimer );
			}

			_countdownTimer = RunTimer( runtime );

			StartCoroutine( _countdownTimer );
		}

		private IEnumerator RunTimer( float seconds )
		{
			float timer = 0f;

			while ( seconds > 0f )
			{
                yield return 0;
				seconds -= Time.deltaTime;
				Debug.Log( "Seconds is: " + seconds );
				
				string timeString = seconds.ToString
				text
			}
		}
	}
}
