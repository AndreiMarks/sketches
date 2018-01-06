using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maximiner
{
	public class LocationHandler : MaximinerBehaviour
	{
		[SerializeField] private List<Location> _locations;
		
		private Location _currentLocation;

		public List<Location> GetNeighboringLocations(Location location)
		{
			SolarSystem system = location.SolarSystem;
			return _locations.Where(l => l.Id != location.Id && l.SolarSystem == system).ToList();	
		}
		
		public void SetLocationById(string locationId)
		{
			_currentLocation = _locations.FirstOrDefault(li => li.Id == locationId);
			Debug.Log("Your current location is: " + _currentLocation.Id);
			
			_Events.ReportLocationChanged(_currentLocation);
		}
	}
}
