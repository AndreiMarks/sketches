using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
    public class MaximinerBehaviour : AmBehaviour
    {
        protected Maximiner _Maximiner
        {
            get { return Maximiner.Instance; }
        }
        
        protected CanvasController _Canvas
        {
            get { return CanvasController.Instance; }
        }

        protected EventsController _Events
        {
            get { return EventsController.Instance; }
        }

        protected MaximinerMapController _Map
        {
            get { return MaximinerMapController.Instance; }
        }

        protected LocationHandler _Locations
        {
            get { return _Maximiner.LocationHandler; }
        }
        
        protected ShipHandler _Ship
        {
            get { return _Maximiner.ShipHandler; }
        }
    }
}
