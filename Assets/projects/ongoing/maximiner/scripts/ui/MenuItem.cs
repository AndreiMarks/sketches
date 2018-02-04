using UnityEngine;

namespace Maximiner
{
	public class MenuItem<T> : MaximinerBehaviour where T : class
	{
		public virtual void Initialize(T menuItemContent)
		{
		}
	}
}
