using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xibao
{
	public class PlayerButtons : XBBehaviour
	{
		public Button mainButton;
		public Text mainButtonText;

		public void SubscribeToEvents()
		{
			GameHandler.OnRoundPhaseChanged += OnRoundPhaseChanged;
		}

		void OnRoundPhaseChanged( RoundPhase newPhase )
		{
			switch ( newPhase )
			{
				case ( RoundPhase.Precursor ):
					SetPrepareButton();
					break;
                case ( RoundPhase.Active ):
	                SetActivateButton();
                    break;
                case ( RoundPhase.Product ):
	                SetAdvanceButton();
                    break;
			}
		}

		void SetPrepareButton()
		{
			mainButton.onClick.RemoveAllListeners();
			mainButtonText.text = "Prepare";
            mainButton.onClick.AddListener( OnPrepareButtonClicked );
		}

		void SetActivateButton()
		{
			mainButton.onClick.RemoveAllListeners();
			mainButtonText.text = "Activate";
            mainButton.onClick.AddListener( OnActivateButtonClicked );
		}

		void SetAdvanceButton()
		{
			mainButton.onClick.RemoveAllListeners();
			mainButtonText.text = "Advance";
            mainButton.onClick.AddListener( OnAdvanceButtonClicked );
		}
		
		void OnPrepareButtonClicked()
		{
			_GameHandler.GoToActivePhase();
		}

		void OnActivateButtonClicked()
		{
			_GameHandler.GoToProductPhase();
		}

		void OnAdvanceButtonClicked()
		{
			_GameHandler.GoToNextTransition();
		}
	}
}
