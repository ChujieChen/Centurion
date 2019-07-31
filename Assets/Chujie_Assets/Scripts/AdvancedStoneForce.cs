using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedStoneForce : MonoBehaviour
{
    public AudioClip  shootSound;
    public GameObject stonePrefab;
    public GameObject stoneTarget;
    public GameObject particleSystemPrefab;

    public GameObject crosshair;

    public float shootingAngle = 45f;

    public float minRange = 8f;
    public float maxRange = 100f;

    Vector3 velocity;
    Vector3 defaultVelcotity;
    int selectedPreset = 0;

    private float g = 9.81f;

    void Start()
    {
        Vector3 Range = (crosshair.transform.position - this.transform.position);
        float Speed = CalculateSpeed(shootingAngle, Range);
        velocity = CalculateVelocity(Speed, shootingAngle, Range);
        print("defaultRange:");
        print(Speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GameObject p = GameObject.FindGameObjectWithTag("Player");
        //float alphaFactor;
        //if (!p || Vector3.Distance(this.transform.position, p.transform.position) > 50)
        //{
        //    alphaFactor = 0f;
        //} else {
        //    alphaFactor = 1f;
        //}
        Vector3 Range = (crosshair.transform.position - this.transform.position);
        if (Input.GetKey(KeyCode.W))
        {
            if (Range.magnitude < maxRange)
            {
                crosshair.transform.position = crosshair.transform.position +
                (+10f * Range.normalized * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Range.magnitude > minRange)
            {
                crosshair.transform.position = crosshair.transform.position +
                (-10f * Range.normalized * Time.deltaTime);
            }
        }
        float Speed = CalculateSpeed(shootingAngle, Range);
        velocity = CalculateVelocity(Speed, shootingAngle, Range);


        //for (int i = 0; i < keyCodes.Length; i++)
        //{
        //    if (Input.GetKeyUp(keyCodes[i]))
        //    {
        //        selectedPreset = i;
        //    }

        //    SpriteRenderer sr = crosshair.GetComponent<SpriteRenderer>();
        //    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.4f * alphaFactor);

        //}
        //SpriteRenderer sr2 = crosshair.GetComponent<SpriteRenderer>();
        //sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, 1f * alphaFactor);
        //forceVector = forcePresets[selectedPreset];
    }

    public void ApplyForce()
    {
        //anim.enabled = false;
        GameObject stone = Instantiate(stonePrefab, stoneTarget.transform.position, stoneTarget.transform.rotation);

        stone.AddComponent<StoneCollision>();
        stone.GetComponent<StoneCollision>().particleSystemPrefab = particleSystemPrefab;
        //stone.GetComponent<Rigidbody>().AddRelativeForce(forceVector.x, forceVector.y, forceVector.z);
        stone.GetComponent<Rigidbody>().velocity = velocity;
        //print("Force applied.");
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    private float CalculateSpeed(float angle, Vector3 range)
    {
        float speed = Mathf.Sqrt(g * range.magnitude /
            (2 * Mathf.Sin(angle * Mathf.Deg2Rad)
            * Mathf.Cos(angle * Mathf.Deg2Rad)));
        return speed;
    }

    private Vector3 CalculateVelocity(float speed, float angle, Vector3 range)
    {
        Vector3 unitVector = range.normalized;
        Vector3 velcocity = unitVector * speed * Mathf.Cos(angle * Mathf.Deg2Rad);
        velcocity = velcocity + new Vector3(0,
            speed * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        return velcocity;
    }
}
