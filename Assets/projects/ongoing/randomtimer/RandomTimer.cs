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
        
        void Start()
        {
            Debug.Log("Hello");
        }

        public void StartTimer()
        {
            Debug.Log("Starting timer.");
            int min = ValidateMin();
            int max = ValidateMax();
            int randomMinutes = Random.Range( min, max );
            
            timer.StartTimer( randomMinutes * 60 );
        }

        public void ResetTimer()
        {
            Debug.Log("Resetting timer."); 
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

