using UnityEngine;
using System.Collections;

public class AudioController : Controller<AudioController>
{
    private AudioSource _audio;
    private AudioSource _Audio
    {
        get
        {
            if ( _audio == null ) _audio = gameObject.AddComponent<AudioSource>();
            return _audio;
        }
    }

    public void PlaySFX( AudioClip audioClip )
    {
        _Audio.PlayOneShot( audioClip );    
    }
}
