using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class ShipHandler : MaximinerBehaviour
	{
		private Ship _currentShip;
		
		public void CreateShip()
		{
			_currentShip = new Ship();		
			_Events.ReportShipChanged(_currentShip);
		}
	}
}
