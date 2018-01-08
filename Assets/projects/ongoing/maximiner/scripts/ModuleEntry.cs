using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maximiner
{
    public class ModuleEntry : MenuItem<Module>
    {
        [SerializeField] private CargoModuleEntry _cargoModulePrefab;
        [SerializeField] private MiningModuleEntry _miningModulePrefab;
        
        public override void Initialize(Module module)
        {
            switch (module.Type)
            {
                case (ModuleType.Cargo):
                    CargoModuleEntry cargo = this.transform.InstantiateChild(_cargoModulePrefab);
                    ZeroOut(cargo.transform);
                    cargo.Initialize((CargoModule)module);
                break;
                
                case (ModuleType.Mining):
                    MiningModuleEntry mining = this.transform.InstantiateChild(_miningModulePrefab);
                    ZeroOut(mining.transform);
                    mining.Initialize((MiningModule)module);
                break;
            }
                
        }

        private void ZeroOut(Transform t)
        {
            RectTransform rt = (RectTransform) t.transform;
            rt.ZeroOut();
            rt.sizeDelta = Vector2.zero;
        }
    }
}
