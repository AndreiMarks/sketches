using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xibao
{
	public class UIHandler : MonoBehaviour
	{
		public Text debugText; 
		
		public void Init()
		{
			GameHandler.OnPhaseChanged += OnPhaseChanged;
		}

		private void OnPhaseChanged( Phase phase )
		{
			debugText.text = phase.ToString();
		}
	}
}
