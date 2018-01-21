using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			EventsController.OnAsteroidMiningStarted += OnAsteroidMiningStarted;
			EventsController.OnAsteroidMiningStopped += OnAsteroidMiningStopped;
			EventsController.OnLocationChanged += OnLocationChanged;
		}
		
		void OnDisable()
		{
			EventsController.OnAsteroidMiningStarted -= OnAsteroidMiningStarted;
			EventsController.OnAsteroidMiningStopped -= OnAsteroidMiningStopped;
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

		public void OnAsteroidMiningStarted(Asteroid asteroid, MiningModule module)
		{
			List<AsteroidEntry> asteroidEntries = _asteroidFieldMenu.GetMenuItems();
			foreach (AsteroidEntry ae in asteroidEntries)
			{
				if (ae.AsteroidId == asteroid.Id)
				{
					ae.SetMiningStatusDisplay(isBeingMined: true);
				}
			}
		}

		public void OnAsteroidMiningStopped(Asteroid asteroid, MiningModule module)
		{
			List<AsteroidEntry> asteroidEntries = _asteroidFieldMenu.GetMenuItems()
																	.Where(ae => ae.AsteroidId == asteroid.Id)
																	.ToList();
			foreach (AsteroidEntry ae in asteroidEntries)
			{
				ae.SetMiningStatusDisplay(isBeingMined: false);
			}
			
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
