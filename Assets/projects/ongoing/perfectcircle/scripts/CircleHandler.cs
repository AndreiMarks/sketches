using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerfectCircle
{
	public class CircleHandler : MonoBehaviour
	{
        public CircleUnit circleUnitPrefab;

		private IEnumerator _currentCircleActivity;
		private CircleUnit _currentCircle;
		
		public void DrawCircleUnit( Vector3 position )
		{
			_currentCircle = transform.InstantiateChild( circleUnitPrefab, position );
			
			_currentCircleActivity = GrowCircle( _currentCircle );
			StartCoroutine( _currentCircleActivity );
		}

		public void StopDrawingCurrentCircle()
		{
			StopCoroutine( _currentCircleActivity );
		}

		private IEnumerator GrowCircle( CircleUnit circle )
		{
			float radiusAmount = 10f;
			
			while ( true )
			{
				circle.IncreaseRadius( radiusAmount );
				yield return 0;
			}
		}
	}
}
