using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	[CreateAssetMenu(fileName = "SpaceStation", menuName = "Space Station", order = 0)]
	public class SpaceStation : Location
	{
		public override void ExitLocation()
		{
			Debug.Log("Exiting the space station.");
		}
	}
}
