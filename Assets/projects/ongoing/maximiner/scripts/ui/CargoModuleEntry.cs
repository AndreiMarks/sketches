using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
    public class CargoModuleEntry : MenuItem<CargoModule>
    {
        [SerializeField] private Text _text;

        private CargoModule _cargoModule;

        void OnEnable()
        {
            EventsController.OnCargoAddedContents += OnCargoAddedContents;
        }
        
        void OnDisable()
        {
            EventsController.OnCargoAddedContents -= OnCargoAddedContents;
        }
        
        public override void Initialize(CargoModule cargoModule)
        {
            _cargoModule = cargoModule;
            SetText(_cargoModule.AvailableVolume.ToString());
        }
        
        public void SetText(string amount)
        {
            _text.text = amount;
        }

        public void OnCargoAddedContents(CargoAddedInfo cargoAddedInfo)
        {
            if (cargoAddedInfo.module.Id != _cargoModule.Id) return;
            
            SetText(_cargoModule.AvailableVolume.ToString());
        }
    }
}
