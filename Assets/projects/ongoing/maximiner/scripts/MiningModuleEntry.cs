using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
    public class MiningModuleEntry : MenuItem<MiningModule>
    {
        [SerializeField] private Text _text;

        public override void Initialize(MiningModule miningModule)
        {
            _text.text = miningModule.Order.ToString();
        }
    }
}
