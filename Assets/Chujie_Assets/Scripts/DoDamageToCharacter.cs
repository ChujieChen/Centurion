using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageToCharacter : MonoBehaviour
{
    public int hitDamage = 200;
    public int rangeDamage = 10;
    public int hitDamageToPlayer = 45;
    public int rangeDamageToPlayer = 8;
    public float rangeDamagePeriod = 1.0f;

    private float timer = 0f;
    public bool solidCollider = true;
    public float impulseFromHit_y = 2.0f;
    public float impulseFromFire_y = 3.0f;

    public AudioClip hitSound;
    public AudioClip rangeSound;


    //private IEnumerator rangeAttack;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider c)
    {
        if ((c.gameObject.tag.Equals("teamA") || c.gameObject.tag.Equals("teamB") || c.gameObject.tag.Equals("spawner"))
&& solidCollider)
        {
            CharacterStats targetStatus = c.gameObject.GetComponent<CharacterStats>();
            targetStatus.getDamage(hitDamage);
            Debug.Log(c.name + hitDamage);
            Vector3 impulseFromHit =
                    new Vector3(Random.Range(-0.2f * impulseFromHit_y, 0.2f * impulseFromHit_y), impulseFromHit_y, Random.Range(-0.2f * impulseFromHit_y, 0.2f * impulseFromHit_y));
            c.gameObject.GetComponent<Rigidbody>().AddForce(impulseFromHit, ForceMode.Impulse);
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
        if(c.gameObject.tag.Equals("Player") && solidCollider){
            CharacterStats targetStatus = c.gameObject.GetComponent<CharacterStats>();
            targetStatus.getDamage(hitDamageToPlayer);
            Vector3 impulseFromHit =
                    new Vector3(Random.Range(-0.2f * impulseFromHit_y, 0.2f * impulseFromHit_y), impulseFromHit_y, Random.Range(-0.2f * impulseFromHit_y, 0.2f * impulseFromHit_y));
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }


        
    }

    //private IEnumerator periodicDamage(float waitTime, CharacterStats targetStatus)
    //{
    //    while (true)
    //    {
    //        targetStatus.TakeDamege(rangeDamage);
    //    }
    //}

    private void OnTriggerStay(Collider c)
    {
        if ((c.gameObject.tag.Equals("teamA") || c.gameObject.tag.Equals("teamB") || c.gameObject.tag.Equals("spawner"))
            && !solidCollider)
        {
            if (timer >= rangeDamagePeriod)
            {
                timer -= rangeDamagePeriod;
                CharacterStats targetStatus = c.gameObject.GetComponent<CharacterStats>();
                targetStatus.getDamage(rangeDamage);
                Vector3 impulseFromFire =
                    new Vector3(Random.Range(-0.2f * impulseFromFire_y, 0.2f * impulseFromFire_y), impulseFromFire_y, Random.Range(-0.2f * impulseFromFire_y, 0.2f * impulseFromFire_y));
                c.gameObject.GetComponent<Rigidbody>().AddForce(impulseFromFire, ForceMode.Impulse);
                GetComponent<AudioSource>().PlayOneShot(rangeSound);
            }
            timer += Time.deltaTime;
        }
        else if(c.gameObject.tag.Equals("Player") && !solidCollider){
            if (timer >= rangeDamagePeriod)
            {
                timer -= rangeDamagePeriod;
                CharacterStats targetStatus = c.gameObject.GetComponent<CharacterStats>();
                targetStatus.getDamage(rangeDamageToPlayer);
                Vector3 impulseFromFire =
                    new Vector3(Random.Range(-0.2f * impulseFromFire_y, 0.2f * impulseFromFire_y), impulseFromFire_y, Random.Range(-0.2f * impulseFromFire_y, 0.2f * impulseFromFire_y));
                c.gameObject.GetComponent<Rigidbody>().AddForce(impulseFromFire, ForceMode.Impulse);
                GetComponent<AudioSource>().PlayOneShot(rangeSound);
            }
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        timer = 0;
    }
}