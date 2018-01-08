using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	[CreateAssetMenu(fileName = "WarpGate", menuName = "Warp Gate", order = 0)]
	public class WarpGate : Location
	{
		public override void ExitLocation()
		{
			Debug.Log("Exiting the warp gate.");
		}
	}
}
