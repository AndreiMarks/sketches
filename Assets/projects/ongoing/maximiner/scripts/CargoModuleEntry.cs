using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
    public class CargoModuleEntry : MenuItem<CargoModule>
    {
        [SerializeField] private Text _text;

        public override void Initialize(CargoModule menuItemContent)
        {
            _text.text = menuItemContent.Size.ToString();
        }
    }
}
