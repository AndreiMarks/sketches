using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
	public class NavigationLocationEntry : MenuItem<Location>
	{
		[SerializeField] private Text _locationNameText;

		private Location _currentLocation;

		public override void Initialize(Location location)
		{
			_currentLocation = location;
			_locationNameText.text = _currentLocation.Name;
		}

		public void OnButtonClicked()
		{
			_Maximiner.MoveToLocation(_currentLocation);
		}
	}
}
