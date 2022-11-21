using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardbassPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.loop = false;
    }

    private AudioClip GetRandomCLip()
    {
        return clips[Random.Range(0, clips.Length)];    
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        {
            source.clip = GetRandomCLip();
            source.Play();
        }
    }
}
