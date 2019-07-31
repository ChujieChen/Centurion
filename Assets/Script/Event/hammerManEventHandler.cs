using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerManEventHandler : MonoBehaviour, AttackEventInterface
{

    public GameObject attackBox;
    public AudioClip missedAttack;
    public AudioClip hitAttack;
    AudioSource audioSource;
    bool justHit = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void hammerManAttackStart()
    {
        attackBox.GetComponent<BoxCollider>().enabled = true;
        justHit = false;
    }


    public void hit()
    {
        justHit = true;
    }
    public void hammerManAttackEnd()
    {
        attackBox.GetComponent<BoxCollider>().enabled = false;
        if (audioSource.isPlaying)
            return;
        if (justHit)
            audioSource.PlayOneShot(hitAttack);
        else
            audioSource.PlayOneShot(missedAttack);
    }
    public void sepcialEffect(Collider collider)
    {
        Vector3 direction = this.transform.forward;
        //Debug.Log(direction);
        collider.gameObject.GetComponent<Rigidbody>().AddForce(direction * 5, ForceMode.Impulse);
    }
}
