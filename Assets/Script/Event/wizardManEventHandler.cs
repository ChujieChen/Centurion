using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizardManEventHandler : MonoBehaviour
{
    public GameObject wand;
    public AudioClip fireSound;
    AudioSource audioSource;
    // Start is called before the first frame update
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void FireEvent()
    {
        Vector3 pos = wand.transform.position + this.transform.forward;
        GameObject fireBall = Instantiate(Resources.Load("Prefabs/SceneController/FireBall") as GameObject);
        fireBall.transform.position = pos;
        fireBall.transform.forward = this.transform.forward;
        fireBall.GetComponent<BulletEvent>().setParent(this.transform.gameObject);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(fireSound);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
