using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xibao
{
	public class StagePositioner : MonoBehaviour
	{
		public float maxRadius;

		private Vector3 _anchorPosition;
		private List<MonadDisplay> _displays = new List<MonadDisplay>();

		void Update()
		{
			UpdateDisplayPositions();
		}

		private void UpdateDisplayPositions()
		{
			float rotateSpeed = 100f;
			for ( int i = 0; i < _displays.Count; i++ )
			{
				_displays[i].RotateAround( _anchorPosition, Vector3.up, rotateSpeed * Time.deltaTime );
				_displays[i].UpdatePosition();
			}
		}
		
		public void AddDisplay( MonadDisplay newDisplay, Vector3 anchorPosition )
		{
			_displays.Add( newDisplay );
			_anchorPosition = anchorPosition;
			
			Vector3 startPos = _anchorPosition + Random.insideUnitSphere * maxRadius;
			newDisplay.SetTargetPosition( startPos, maxRadius );
		}

		public void AddDisplays( List<MonadDisplay> newDisplays, Vector3 anchorPosition )
		{
			_displays.AddRange( newDisplays );
			for ( int i = 0; i < newDisplays.Count; i++ )
			{
				newDisplays[i].SetTargetPosition( anchorPosition + Random.insideUnitSphere * maxRadius, maxRadius );
			}
			_anchorPosition = anchorPosition;
		}

		public void ClearDisplays()
		{
			for ( int i = _displays.Count - 1; i >= 0; i-- )
			{
				Destroy( _displays[i].gameObject );
			}
			
			_displays.Clear();
		}
		
		public List<MonadDisplay> TakeCurrentMonads()
		{
			List<MonadDisplay> monads = new List<MonadDisplay>( _displays );
			_displays.Clear();
			return monads;
		}
	}
}
