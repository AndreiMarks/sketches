using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
    public class ShipDisplay : MaximinerBehaviour
    {
        public PositionInfo[] displayInfos;
        [SerializeField] private ModuleMenu _moduleMenu;

        void OnEnable()
        {
            EventsController.OnShipChanged += OnShipChanged;
        }
        
        void OnDisable()
        {
            EventsController.OnShipChanged -= OnShipChanged;
        }
        
        public void MoveDisplayToCenter()
        {
            MoveDisplayToPosition(Position.Center);
        }

        public void MoveDisplayToBottom()
        {
            MoveDisplayToPosition(Position.Bottom);
        }

        private void MoveDisplayToPosition(Position position)
        {
            foreach (PositionInfo pi in displayInfos)
            {
                switch (position)
                {
                    case (Position.Center):
                        pi.transform.anchoredPosition = pi.centerPosition;
                        break;

                    case (Position.Bottom):
                        pi.transform.anchoredPosition = pi.bottomPosition;
                        break;
                }
            }
        }

        private void OnShipChanged(Ship ship)
        {
            List<Module> modules = new List<Module>();
             
            modules.Add(new CargoModule(ship.CargoHoldVolume));
             
            for (int i = 0; i < ship.MiningLaserCount; i++)
            {
                modules.Add(new MiningModule());
            }
             
            _moduleMenu.AddMenuItems(modules);
        }

        private enum Position
        {
            Center,
            Bottom
        }

        [System.Serializable]
        public class PositionInfo
        {
            public RectTransform transform;
            public Vector2 centerPosition;
            public Vector2 bottomPosition;
        }
    }
}
