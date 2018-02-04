using Random = UnityEngine.Random;
using System;
using UnityEngine;

namespace Maximiner
{
	public class Asteroid
	{
		public int Id { get; private set; }
		
		private string _name;
		public string Name
		{
			get { return Type.ToString() + " " + Class.ToString(); }
		}

		public AsteroidType Type { get; private set; }
		public AsteroidClass Class { get; private set; }

		public float TotalVolume { get; private set; }

		public float OreVolume
		{
			get { return Type.GetVolume(); }
		}

		public Asteroid(bool isRandom)
		{
			if (isRandom)
			{
				Id = GetHashCode();
				_name = Id.ToString();
				Type = (AsteroidType)Random.Range(0, Enum.GetValues(typeof(AsteroidType)).Length);// as AsteroidType;
				Class = (AsteroidClass)Random.Range(0, Enum.GetValues(typeof(AsteroidClass)).Length);

				TotalVolume = Random.Range(500, 5000);
			}
		}

		public void RemoveOre(OreYieldInfo oyi)
		{
			Debug.Log("Removing Ore from Asteroid.");
			TotalVolume -= oyi.volume;
			
			//TODO Need to handle if the Asteroid goes away.
		}
		
		#region Utility Methods --------------------------------------------------
		
		public override string ToString()
		{
			return "Asteroid " + _name + " " + Name;
		}
		
		#endregion
	}

	public enum AsteroidType
	{
		Alpha,
		Beta,
		Gamma
	}

	public enum AsteroidClass
	{
		I,
		II,
		III
	}
	
}
