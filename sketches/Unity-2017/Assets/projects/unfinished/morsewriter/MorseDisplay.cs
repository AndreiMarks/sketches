using UnityEngine;
using System.Collections;

public class MorseDisplay : MonoBehaviour 
{
    public float speed;
    public float lerpSpeed;
    public float yMin = .25f;
    public float yMax = 2.5f;

    public ParticleSystem particles;

    private float _currentZ = 0f;

	void Update () 
    {
        Rotate();	
	}

    private void Rotate()
    {
        _currentZ += lerpSpeed * Time.deltaTime;
        if ( _currentZ >= yMax ) _currentZ = yMin;
        particles.transform.localPosition = Vector3.up * _currentZ;

        transform.Rotate( Vector3.forward * speed * Time.deltaTime );
    }

    public void StartDisplay()
    {
        particles.Play();
    }

    public void StopDisplay()
    {
        particles.Stop();
    }
}
