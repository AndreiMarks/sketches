using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	[CreateAssetMenu(fileName = "Location", menuName = "Location", order = 0)]
	public class Location : ScriptableObject
	{
		[SerializeField] 
		private string _id;
		public string Id
		{
			get { return _id; }
		}
		
		[SerializeField] 
		private string _name;
		public string Name
		{
			get { return _name; }
		}

		[SerializeField] private SolarSystem _solarSystem;
		public SolarSystem SolarSystem
		{
			get { return _solarSystem; }	
		}

		public virtual void EnterLocation()
		{
		}
		
		public virtual void ExitLocation()
		{
		}
	}
}
