using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

namespace Maximiner
{
	public class CargoModule : Module
	{
		public override int Id { get; protected set; }

		public override ModuleType Type
		{
			get { return ModuleType.Cargo; }
		}
		
		private float _size;
		public float Size
		{
			get { return _size; }
		}

		public float AvailableVolume
		{
			get { return 100f; }
		}

		public CargoModule(float size)
		{
			_size = size;
		}
	}
}
