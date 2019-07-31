using UnityEngine;
using System.Collections;

public class BoulderSound : MonoBehaviour
{

    public AudioClip crashHard;


    private AudioSource source;
    //private float lowPitchRange = .75F;
    //private float highPitchRange = 1.5F;
    //private float velToVol =20F;
    //private float velocityClipSplit = 10F;


    void Awake()
    {

        source = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag.Equals("teamA") || coll.gameObject.tag.Equals("teamB"))
            GetComponent<AudioSource>().PlayOneShot(crashHard);
    }

}
