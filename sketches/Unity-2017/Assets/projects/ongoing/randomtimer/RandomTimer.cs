using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RandomTimer
{
    public class RandomTimer : MonoBehaviour
    {
        private const int _MIN_DEFAULT = 10;
        private const int _MAX_DEFAULT = 30;

        public Timer timer;
        
        public InputField minField;
        public InputField maxField;

        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _audioFinishedSFX;
        
        void Start()
        {
            Debug.Log("Hello");
            LocalNotifier.RegisterNotifications();
        }

        public void StartTimer()
        {
            LocalNotifier.ClearAlertNotifications();
            
            float min = (float)ValidateMin();
            float max = (float)ValidateMax();
            float randomMinutes = Random.Range(min, max);

            float totalSeconds = randomMinutes * 60;
            timer.StartTimer(totalSeconds, DoTimerFinished);
            
            LocalNotifier.ScheduleAlertNotification((int)totalSeconds);
        }

        public void ResetTimer()
        {
            timer.StopTimer();
            LocalNotifier.ClearAlertNotifications();
        }

        private void DoTimerFinished()
        {
            PlayTimerFinishedSFX();    
        }
        
        private void PlayTimerFinishedSFX()
        {
            _audio.PlayOneShot(_audioFinishedSFX);
        }

        private int ValidateMin()
        {
            int min;
            if ( int.TryParse( minField.text, out min ) )
            {
                return min;
            }
            
            return _MIN_DEFAULT;
        }

        private int ValidateMax()
        {
            int max;
            if ( int.TryParse( maxField.text, out max ) )
            {
                return max;
            }
            
            return _MAX_DEFAULT;
        }
    }
}

