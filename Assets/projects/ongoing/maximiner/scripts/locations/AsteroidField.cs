using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	[CreateAssetMenu(fileName = "AsteroidField", menuName = "Asteroid Field", order = 0)]
	public class AsteroidField : Location
	{
		[SerializeField] private bool shouldPopulateRandomly;
		
		private List<Asteroid> _asteroids;

		public List<Asteroid> Asteroids
		{
			get
			{
				if (_asteroids == null)
				{
					_asteroids = CreateAsteroids();
				}
				return _asteroids;
			}
		}

		private List<Asteroid> CreateAsteroids()
		{
			List<Asteroid> asteroids = new List<Asteroid>();
			
            if (shouldPopulateRandomly)
            {
	            int count = Random.Range(1, 50);
	            for (int i = 0; i < count; i++)
	            {
		            asteroids.Add(new Asteroid(isRandom:true));
	            }        
            }

			return asteroids;
		}
		
		public override void ExitLocation()
		{
			Debug.Log("Exiting the asteroid field.");
		}
	}
}
