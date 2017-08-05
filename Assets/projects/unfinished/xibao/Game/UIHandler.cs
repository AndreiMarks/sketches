using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xibao
{
	public class UIHandler : MonoBehaviour
	{
		public Text debugText;
		public PlayerButtons playerButtons;
		
		public void Init()
		{
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			GameHandler.OnPhaseChanged += OnPhaseChanged;

			playerButtons.SubscribeToEvents();
		}
		
		private void OnPhaseChanged( Phase phase )
		{
			debugText.text = phase.ToString();
		}
	}
}
