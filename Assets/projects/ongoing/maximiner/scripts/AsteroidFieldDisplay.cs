using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maximiner
{
	public class AsteroidFieldDisplay : MaximinerBehaviour
	{
		[SerializeField] private RectTransform _asteroidHolder;
		[SerializeField] private AsteroidFieldMenu _asteroidFieldMenu;

		private Location _currentLocation;
		
		void OnEnable()
		{
			EventsController.OnLocationChanged += OnLocationChanged;
		}
		
		void OnDisable()
		{
			EventsController.OnLocationChanged -= OnLocationChanged;
		}

		void Awake()
		{
			_asteroidHolder.gameObject.SetActive(false);
		}

		private void PopulateAsteroidEntries(AsteroidField field)
		{
			_asteroidFieldMenu.ClearMenuItems();
			_asteroidFieldMenu.AddMenuItems(field.Asteroids);
		}
		
		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;

			AsteroidField field = location as AsteroidField;
			if (field != null)
			{
				_asteroidHolder.gameObject.SetActive(true);
				PopulateAsteroidEntries(field);
			}
			else
			{
				_asteroidHolder.gameObject.SetActive(false);
			}
		}
	}
}
