using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xibao
{
	public class InputHandler : MonoBehaviour
	{
		public bool WaitForAnyInput()
		{
			return Input.anyKeyDown;
		}
	}
}
