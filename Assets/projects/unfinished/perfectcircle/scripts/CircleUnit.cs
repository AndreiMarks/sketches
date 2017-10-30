using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDesigner;

namespace PerfectCircle
{
	public class CircleUnit : MonoBehaviour
	{
        public Circle circle;

		public float Radius
		{
			get { return circle.radius; }
		}
		
		public void DecreaseRadius( float decreaseAmount )
		{
			circle.radius -= decreaseAmount * Time.deltaTime;
		}

		public void IncreaseRadius( float increaseAmount )
		{
			circle.radius += increaseAmount * Time.deltaTime;
		}
	}
}
