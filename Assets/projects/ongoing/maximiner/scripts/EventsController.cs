using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class EventsController : Controller<EventsController>
	{
		public static event Action<Asteroid> OnAsteroidEntryClicked = (asteroid) => { };
		public void ReportAsteroidEntryClicked(Asteroid asteroid)
		{
			OnAsteroidEntryClicked(asteroid);
		}
		
		public static event Action<Asteroid, MiningModule> OnAsteroidMiningStarted= (asteroid, miningModule) => { };
		public void ReportAsteroidMiningStarted(Asteroid asteroid, MiningModule miningModule)
		{
			OnAsteroidMiningStarted(asteroid, miningModule);
		}
		
		public static event Action<Asteroid, MiningModule> OnAsteroidMiningStopped= (asteroid, miningModule) => { };
		public void ReportAsteroidMiningStopped(Asteroid asteroid, MiningModule miningModule)
		{
			OnAsteroidMiningStopped(asteroid, miningModule);
		}
		
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
