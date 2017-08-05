using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDesigner;

namespace PerfectCircle
{
	public class CircleUnit : MonoBehaviour
	{
        public Circle circle;

		public void IncreaseRadius( float increaseAmount )
		{
			circle.radius += increaseAmount * Time.deltaTime;
		}
	}
}
