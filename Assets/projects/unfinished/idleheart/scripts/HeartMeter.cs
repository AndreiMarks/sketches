using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDesigner;

public class HeartMeter : MonoBehaviour 
{
    [SerializeField] Circle _circle;
    [SerializeField] float _toAngleMin;
    [SerializeField] float _toAngleMax;

	public void UpdateMeter( float ratio ) 
    {
		_circle.toAngle = Mathf.Lerp( _toAngleMin, _toAngleMax, ratio );
	}
}
