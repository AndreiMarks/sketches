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
        }

        public void MoveDisplayToBottom()
        {
        }

        private void MoveDisplayToPosition(Position position)
        {
        }

        private void OnShipChanged(Ship ship)
        {
            _moduleMenu.AddMenuItems(ship.modules);
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
