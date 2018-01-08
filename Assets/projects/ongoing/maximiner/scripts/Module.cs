using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public abstract class Module : MonoBehaviour
	{
		public abstract ModuleType Type { get; }
	}

	public enum ModuleType
	{
		Cargo,
		Mining
	}
}
