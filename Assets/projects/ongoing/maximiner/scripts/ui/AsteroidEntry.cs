using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
	public class AsteroidEntry : MenuItem<Asteroid>
	{
		[SerializeField] private Image _asteroidImageBackground;
		[SerializeField] private Text _asteroidDesignator;
		
		[SerializeField] private Color _activeColor;
		[SerializeField] private Color _inactiveColor;

		private Asteroid _asteroid;

		public int AsteroidId
		{
			get { return _asteroid.Id; }
		}

		public override void Initialize(Asteroid asteroid)
		{
			_asteroid = asteroid;
			_asteroidDesignator.text = asteroid.Name;
		}

		public void SetMiningStatusDisplay(bool isBeingMined)
		{
			Color color = isBeingMined ? _activeColor : _inactiveColor;
			_asteroidImageBackground.color = color;
		}

		public void OnButtonClicked()
		{
			_Events.ReportAsteroidEntryClicked(_asteroid);
		}
	}
}
