using System;
using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

namespace Maximiner
{
	public class MiningModule : Module
	{
		public override int Id { get; protected set; }

		public override ModuleType Type
		{
			get { return ModuleType.Mining; }
		}

		public bool IsActivated { get; private set; }

		public int Order { get; private set; }

		private Asteroid _target;
		public Asteroid Target
		{
			get { return _target; }
		}

		public MiningModule(int order)
		{
			Id = GetHashCode();
			Order = order;
		}

		public void StartMining(Asteroid asteroid)
		{
			_target = asteroid;
			IsActivated = true;
		}
		
		public void StopMining(Asteroid asteroid)
		{
			_target = null;
			IsActivated = false;
		}
	}
}
