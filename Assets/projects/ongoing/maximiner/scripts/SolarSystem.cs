using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	[CreateAssetMenu(fileName = "SolarSystem", menuName = "Solar System", order = 0)]
	public class SolarSystem : ScriptableObject
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
	}
}
