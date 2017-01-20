using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerfectCircle
{
	public class PCBehaviour : MonoBehaviour
	{
		protected PerfectCircle _PC
		{
			get { return PerfectCircle.Instance; }
		}

		protected Camera _MainCam
		{
			get { return _PC.mainCam; }
		}

		protected CircleHandler _CircleHandler
		{
			get { return _PC.circleHandler; }
		}
	}
}
