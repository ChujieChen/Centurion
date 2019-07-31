using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic: MonoBehaviour
{
    public AudioClip[] clips;
    public float volume = 1f;
    AudioSource a;
    // Start is called before the first frame update
    void Start()
    {
        a = this.GetComponent<AudioSource>();
        playRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (!a.isPlaying)
            playRandom();
    }
    void playRandom()
    {
        int index = Random.Range(0, clips.Length);
        a.PlayOneShot(clips[index], volume);
    }

}
