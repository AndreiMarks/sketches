using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

namespace Maximiner
{
	public class CargoModule : Module
	{
		public override ModuleType Type
		{
			get { return ModuleType.Cargo; }
		}
		
		private int _size;
		public int Size
		{
			get { return _size; }
		}

		public CargoModule(int size)
		{
			_size = size;
		}
	}
}
