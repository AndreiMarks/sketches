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
        public MonadDisplay monadDisplayPrefab;
        public Color color;

        public MonadDisplay GetMonadDisplay( Transform parent )
        {
            MonadDisplay md = parent.InstantiateChild( monadDisplayPrefab );
            md.Initialize( this );
            return md;
        }
    }

    [System.Serializable]
    public class MonadCollection
    {
        public Monad monad;
        public int quantity;
        public bool isPrimary;
    }
}
