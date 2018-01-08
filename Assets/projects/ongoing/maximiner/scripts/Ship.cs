using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class Ship
	{
		public int MiningLaserCount
		{
			get { return 2; }
		}

		public int CargoHoldVolume
		{
			get { return 100; }
		}

		public Ship()
		{
			// Create default ship.
		}
	}
}
