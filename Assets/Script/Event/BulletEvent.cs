using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEvent : MonoBehaviour
{
    public AudioClip hitSound;
    public float range; 
	string parentTag;
    int damage;
    Vector3 pos;
    float lifeTime;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
	{
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward, ForceMode.Impulse);
        pos = this.transform.position;
        lifeTime = GetComponent<ParticleSystem>().startLifetime;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
	{
        float distance = Vector3.Distance(this.transform.position, pos);
        float percent = (1 - distance / range);
        if (percent < 0)
            Destroy(this.transform.gameObject);
        this.GetComponent<ParticleSystem>().startLifetime = percent * lifeTime;

	}
	public void setParent(GameObject p)
	{
        parentTag = string.Copy(p.tag);
        damage = p.GetComponent<CharacterStats>().attackPoint;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isEnemy(parentTag, other.gameObject))
		{
            other.gameObject.GetComponent<CharacterStats>().getDamage(damage, false);
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
	}
	static bool isEnemy(string aTag, GameObject b)
	{
        if ((aTag == "teamA" || aTag == "Player") && (b.tag == "teamB" || b.tag == "spawner"))
        {
            return true;
        }
        if ((b.tag == "teamA" || b.tag == "Player") && (aTag == "teamB" || aTag == "spawner"))
        {
            return true;
        }
        return false;
	}
}
