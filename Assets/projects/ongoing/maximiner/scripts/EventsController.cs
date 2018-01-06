using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
	public class EventsController : Controller<EventsController>
	{
		public static event Action<Location> OnLocationChanged = (location) => { };

		public void ReportLocationChanged(Location location)
		{
			OnLocationChanged(location);
		}
		
	}
}
