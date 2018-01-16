using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
	public class AsteroidEntry : MenuItem<Asteroid>
	{
		[SerializeField] private Text _asteroidDesignator;

		private Asteroid _asteroid;

		public override void Initialize(Asteroid asteroid)
		{
			_asteroid = asteroid;
			_asteroidDesignator.text = asteroid.Name;
		}

		public void OnButtonClicked()
		{
			//_Maximiner.MoveToLocation(_currentLocation);
			Debug.Log(_asteroid.ToString());
			_Events.ReportAsteroidEntryClicked(_asteroid);
		}
	}
}
