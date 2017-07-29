using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xibao
{
    [System.Serializable]
    [CreateAssetMenu]
    public class Monad : ScriptableObject
    {
        public string name;
    }

    [System.Serializable]
    public class MonadCollection
    {
        public Monad monad;
        public int quantity;
        public bool isPrimary;
    }
}
