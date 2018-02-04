using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class ShipHandler : MaximinerBehaviour
	{
		private Ship _currentShip;
		public Ship CurrentShip
		{
			get { return _currentShip; }
		}

		public void CreateShip()
		{
			_currentShip = CreateDefaultShip();

			_Events.ReportShipChanged(_currentShip);
		}

		private Ship CreateDefaultShip()
		{
			Ship ship = new Ship();

			float cargoVolume = 100;            
			ship.AddModule(new CargoModule(cargoVolume));

			int miningLaserCount = 2;
            for (int i = 0; i < miningLaserCount; i++)
            {
                ship.AddModule(new MiningModule(i+1));
            }

			return ship;
		}
	}
}
