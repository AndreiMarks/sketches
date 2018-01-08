using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class MenuPopulator<T,U> : MonoBehaviour where U : class where T : MenuItem<U>
	{
		[SerializeField] private RectTransform _contentHolder;
		[SerializeField] private T _prefab;

		public void AddMenuItems(List<U> items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				U obj = items[i];
				T newItem = _contentHolder.InstantiateChild(_prefab);
				newItem.transform.ZeroOut();
				newItem.Initialize(obj);
			}
		}

		public void ClearMenuItems()
		{
			_contentHolder.DestroyAllChildren();
		}
	}
}
