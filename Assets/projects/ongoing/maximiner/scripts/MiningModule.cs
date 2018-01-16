using System;
using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

namespace Maximiner
{
	public class MiningModule : Module
	{
		public override ModuleType Type
		{
			get { return ModuleType.Mining; }
		}

		public int Order { get; private set; }

		public MiningModule(int order)
		{
			Order = order;
		}
	}
}
