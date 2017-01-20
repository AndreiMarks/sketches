using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RandomTimer
{
	public class Timer : MonoBehaviour
	{
		public Text textField;
		[SerializeField] private AudioSource _audio;
		[SerializeField] private AudioClip[] _audioClips;
		
		private IEnumerator _countdownTimer;

		public void StartTimer( float runtime, Action finishedCallback )
		{
			Debug.Log( "Running timer with " + runtime + " seconds." );
			
			if ( _countdownTimer != null )
			{
				StopCoroutine( _countdownTimer );
			}

			_countdownTimer = RunTimer( runtime, finishedCallback );

			StartCoroutine( _countdownTimer );
		}

		public void StopTimer()
		{
			if ( _countdownTimer != null )
			{
				StopCoroutine( _countdownTimer );
			}
		}
		
		private IEnumerator RunTimer( float seconds, Action finishedCallback )
		{
			DateTime startTime = DateTime.UtcNow;
			DateTime endTime = startTime + TimeSpan.FromSeconds(seconds);

			TimeSpan timeRemaining;
			TimeSpan zeroLimit = new TimeSpan(0, 0, 0, 1);

			int lastSecond = (endTime - DateTime.UtcNow).Seconds;
			
			while ( endTime - DateTime.UtcNow > zeroLimit )
			{
                yield return 0;

				timeRemaining = endTime - DateTime.UtcNow;

				string timeStringFormat = "{0:D2}:{1:D2}:{2:D2}";
				int hours = timeRemaining.Days * 24 + timeRemaining.Hours;
				textField.text = string.Format(timeStringFormat, 
					hours, 
					timeRemaining.Minutes, 
					timeRemaining.Seconds);

				if (timeRemaining.Seconds != lastSecond)
				{
					_audio.PlayOneShot(_audioClips.RandomElement());		
				}
				
				lastSecond = timeRemaining.Seconds;
			}
			
            textField.text = "00:00:00";

			finishedCallback();
		}
	}
}
