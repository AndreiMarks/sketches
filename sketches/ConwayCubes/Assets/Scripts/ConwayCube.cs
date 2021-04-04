using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConwayCube : MonoBehaviour
{
    [SerializeField] private float _lerpSpeed;
    private Quaternion _targetRotation;

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, 
                                              _targetRotation, 
                                              _lerpSpeed * Time.deltaTime);

        /*
        transform.position = new Vector3(transform.position.x,
                                         0f,
                                         transform.position.z) +
                             Vector3.up *
                             Mathf.Sin(Time.time * .1f);
                             */
    }
    
    public void SetOccupied(bool isOccupied)
    {
        if (isOccupied)
        {
            _targetRotation = Quaternion.Euler(Vector3.right * 180f);
        }
        else
        {
            _targetRotation = Quaternion.Euler(Vector3.right * 360f);
        }
    }
}
