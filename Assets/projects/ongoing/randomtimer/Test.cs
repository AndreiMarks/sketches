using System.Collections;
using System.Collections.Generic;
using FS.Handlers;
using UnityEngine;

public class Test : MonoBehaviour
{

	public FSScrollController scroller;

	void Update()
	{
		scroller.ScrollController();
	}
}
