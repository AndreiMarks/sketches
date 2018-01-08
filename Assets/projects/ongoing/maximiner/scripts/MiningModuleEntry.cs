using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
    public class MiningModuleEntry : MenuItem<MiningModule>
    {
        [SerializeField] private Text _text;

        public override void Initialize(MiningModule menuItemContent)
        {
            _text.text = "0";
        }
    }
}
