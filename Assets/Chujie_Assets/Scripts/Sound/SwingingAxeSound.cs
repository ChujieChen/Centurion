using UnityEngine;
using System.Collections;

public class SwingingAxeSound : MonoBehaviour
{

    public AudioClip axeKill;


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
        if(coll.gameObject.tag.Equals("teamA") || coll.gameObject.tag.Equals("teamB") || coll.gameObject.tag.Equals("Player"))
            GetComponent<AudioSource>().PlayOneShot(axeKill);
    }

}
