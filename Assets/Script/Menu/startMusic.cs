using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMusic : MonoBehaviour
{
    public AudioClip startClip;
    public void startGame()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(startClip);
    }
}
