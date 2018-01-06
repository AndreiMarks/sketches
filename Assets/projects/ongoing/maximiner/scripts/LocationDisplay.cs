using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maximiner
{
	public class LocationDisplay : MaximinerBehaviour
	{
		[SerializeField] private Text _locationText;
		[SerializeField] private RectTransform _navigationMenuHolder;
		[SerializeField] private NavigationLocationMenu _navigationMenuContent;

		private Location _currentLocation;
		
		void OnEnable()
		{
			EventsController.OnLocationChanged += OnLocationChanged;
		}
		
		void OnDisable()
		{
			EventsController.OnLocationChanged -= OnLocationChanged;
		}

		public void ToggleNavigationMenu()
		{
			bool shouldBeActive = !_navigationMenuHolder.gameObject.activeInHierarchy;
			_navigationMenuHolder.gameObject.SetActive(shouldBeActive);

			if (shouldBeActive && _currentLocation != null)
			{
				List<Location> availableLocations = _Locations.GetNeighboringLocations(_currentLocation);
				_navigationMenuContent.AddMenuItems(availableLocations);
			}
		}
		
		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
			_locationText.text = location.Name;
		}
	}
}
