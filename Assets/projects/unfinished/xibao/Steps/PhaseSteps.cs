using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xibao;

namespace Xibao
{
    [CreateAssetMenu]
    public class PhaseSteps : ScriptableObject
    {
        public Phase phase;
        public TransitionStep[] steps;
    }
}
