using System.Collections;
using Maximiner;
using UnityEngine;

public class Coroutiner : MaximinerBehaviour
{
    private static Coroutiner _instance;
    public static Coroutiner Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        _instance = this;
    }

    public Coroutine Run(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void Stop(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
