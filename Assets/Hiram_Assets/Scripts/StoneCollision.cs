using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollision : MonoBehaviour
{
    public GameObject particleSystemPrefab;
    bool collidedOnce;

    void Start()
    {
        collidedOnce = false;
    }

    IEnumerator DestroyParticleSystem(GameObject gObj)
    {
        yield return new WaitForSeconds(10);
        print("Destroying...");
        gObj.GetComponent<ParticleSystem>().Stop();
        Destroy(gObj);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider c)
    {
        if (collidedOnce) return;

        //ContactPoint contact = c.contacts[0];
        if (c.gameObject.name == "Terrain")
        {
            collidedOnce = true;
            print("Collided at " + gameObject.transform.position);
            GameObject gObj = Instantiate(particleSystemPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            ParticleSystem ps = gObj.GetComponent<ParticleSystem>();
            ps.Play();
            StartCoroutine(DestroyParticleSystem(gObj));
        }

        foreach (ParticleSystem ps in c.gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            c.gameObject.GetComponent<Renderer>().material.SetFloat("_Fade", 1f);
            ps.Play();
        }
    }
}
