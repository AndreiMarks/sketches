using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Maximiner
{
    public class Maximiner : MaximinerBehaviour
    {
        [SerializeField] private LocationHandler _locationHandler;
        public LocationHandler LocationHandler
        {
            get { return _locationHandler; }
        }
        
        [SerializeField] private ShipHandler _shipHandler;
        public ShipHandler ShipHandler
        {
            get { return _shipHandler; }
        }
        
        private static Maximiner _instance;
        public static Maximiner Instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        
        void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            Debug.Log("Starting Maximiner.");
            ShipHandler.CreateShip();
            LocationHandler.SetLocationById("HomeStation");
        }
        
        public void MoveToLocation(Location newLocation)
        {
			_Locations.SetLocationById(newLocation.Id);
        }

        private void DoOldGame()
        {
            _Canvas.ShowTitleScreen();
            _Map.Initialize();
        }
    }
}
