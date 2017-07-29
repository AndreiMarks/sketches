using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xibao;

namespace Xibao
{
    [CreateAssetMenu]
    public class TransitionStep : ScriptableObject
    {
        public string name;
        public MonadCollection[] precursors;
        public MonadCollection[] products;
    }
}
