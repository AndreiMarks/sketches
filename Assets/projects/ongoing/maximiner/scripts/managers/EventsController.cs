using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class EventsController : Controller<EventsController>
	{
		#region Asteroid Mining Events --------------------------------------------------
		
		public static event Action<Asteroid> OnAsteroidEntryClicked = (asteroid) => { };
		public void ReportAsteroidEntryClicked(Asteroid asteroid)
		{
			OnAsteroidEntryClicked(asteroid);
		}
		
		public static event Action<AsteroidMiningInfo> OnAsteroidMiningStarted = (asteroidMiningInfo) => { };
		public void ReportAsteroidMiningStarted(AsteroidMiningInfo asteroidMiningInfo)
		{
			OnAsteroidMiningStarted(asteroidMiningInfo);
		}
		
		public static event Action<AsteroidMiningInfo> OnAsteroidMiningUpdated = (asteroidMiningInfo) => { };
		public void ReportAsteroidMiningUpdated(AsteroidMiningInfo asteroidMiningInfo)
		{
			OnAsteroidMiningUpdated(asteroidMiningInfo);
		}
		
		public static event Action<AsteroidMiningInfo> OnAsteroidMiningStopped = (asteroidMiningInfo) => { };
		public void ReportAsteroidMiningStopped(AsteroidMiningInfo asteroidMiningInfo)
		{
			OnAsteroidMiningStopped(asteroidMiningInfo);
		}
		
		#endregion
		
		#region Module Related Events --------------------------------------------------
		
		public static event Action<AsteroidMiningInfo> OnAsteroidMiningCycleFinished = (asteroidMiningInfo) => { };
		public void ReportAsteroidMiningCycleFinished(AsteroidMiningInfo asteroidMiningInfo)
		{
			OnAsteroidMiningCycleFinished(asteroidMiningInfo);
		}

		public static event Action<CargoAddedInfo> OnCargoAddedContents = (cargoAddedInfo) => { };
		public void ReportCargoAddedContents(CargoAddedInfo cargoAddedInfo)
		{
			OnCargoAddedContents(cargoAddedInfo);
		}
		
		#endregion
		
		public static event Action<Location> OnLocationChanged = (location) => { };
		public void ReportLocationChanged(Location location)
		{
			OnLocationChanged(location);
		}
		
		public static event Action<Ship> OnShipChanged = (ship) => { };
		public void ReportShipChanged(Ship ship)
		{
			OnShipChanged(ship);
		}
		
		public static event Action<string> OnDoWarningMessage = (message) => { };
		public void ReportDoWarningMessage(string message)
		{
			OnDoWarningMessage(message);
		}
	}
}
