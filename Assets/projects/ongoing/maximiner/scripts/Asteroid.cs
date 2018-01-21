using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Random = UnityEngine.Random;

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

		public Asteroid(bool isRandom)
		{
			if (isRandom)
			{
				Id = GetHashCode();
				_name = Id.ToString();
				Type = (AsteroidType)Random.Range(0, Enum.GetValues(typeof(AsteroidType)).Length);// as AsteroidType;
				Class = (AsteroidClass)Random.Range(0, Enum.GetValues(typeof(AsteroidClass)).Length);
			}
		}

		public override string ToString()
		{
			return "Asteroid " + _name + " " + Name;
		}
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
