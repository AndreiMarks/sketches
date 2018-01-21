using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public abstract class Module
	{
		public abstract int Id { get; protected set; }
		public abstract ModuleType Type { get; }
	}

	public enum ModuleType
	{
		Cargo,
		Mining
	}
}
