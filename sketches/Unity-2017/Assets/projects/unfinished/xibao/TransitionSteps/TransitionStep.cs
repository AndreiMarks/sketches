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
        public MonadCollection[] enzymes;
        public MonadCollection[] precursors;
        public MonadCollection[] products;

        public List<Monad> GetMonadCollection( MonadCollection[] collection )
        {
            List<Monad> monads = new List<Monad>();
            for ( int i = 0; i < collection.Length; i++ )
            {
                MonadCollection mc = collection[i];
                for ( int j = 0; j < mc.quantity; j++ )
                {
                    monads.Add( mc.monad );
                }
            }

            return monads;
        }

        public List<Monad> GetPrecursors()
        {
            return GetMonadCollection( precursors );
        }
        
        public List<Monad> GetProducts()
        {
            return GetMonadCollection( products );
        }
    }
}
