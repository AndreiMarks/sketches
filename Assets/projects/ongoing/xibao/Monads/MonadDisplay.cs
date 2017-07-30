using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xibao
{
    public class MonadDisplay : MonoBehaviour
    {
        public Monad Monad { get; set; }
        public Transform displayTransform;

        private Transform _targetTransform;
        private float _lerpSpeed = 10f;

        public void Initialize( Monad monad )
        {
            Monad = monad;
            displayTransform.GetComponent<Renderer>().material.color = monad.color;
            displayTransform.GetComponent<TrailRenderer>().material.color = monad.color;
        }
        
        public void UpdatePosition()
        {
            displayTransform.position = Vector3.Lerp( displayTransform.position, _targetTransform.position, _lerpSpeed * Time.deltaTime );
        }

        public void RotateAround( Vector3 position, Vector3 axis, float angle )
        {
            _targetTransform.RotateAround( position, _targetTransform.forward, angle );
        }
        
        public void SetTargetPosition( Vector3 position, float maxRadius )
        {
            if ( _targetTransform == null )
            {
                _targetTransform = ( new GameObject( gameObject.name + "Target" ) ).transform;
                _targetTransform.SetParent( this.transform );
                _targetTransform.position = position;
                _targetTransform.up = (_targetTransform.position - transform.position ).normalized;
            }
            
            Vector3 distanceVector = _targetTransform.position - transform.position;
            float distance = distanceVector.magnitude;
            if ( distance > maxRadius ) _targetTransform.position = distanceVector.normalized * maxRadius;
            _targetTransform.position = position;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere( _targetTransform.position, 1f );
        }
    }
}
