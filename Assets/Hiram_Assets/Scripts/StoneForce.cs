using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneForce : MonoBehaviour
{
    public GameObject stonePrefab;
    public GameObject stoneTarget;
    public GameObject particleSystemPrefab;

    public GameObject[] crosshairs;

    Vector3 forceVector;
    Vector3[] forcePresets;
    int selectedPreset = 0;

    void Start()
    {
        forcePresets = new[] {
            new Vector3(400*1.25f, 0, 400*1.25f),
            new Vector3(515*1.29f, 0, 515*1.29f),
            new Vector3(580*1.37f, 0, 580*1.37f),
            new Vector3(660*1.4f, 0, 660*1.4f),
            new Vector3(720*1.43f, 0, 720*1.43f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        float alphaFactor;
        if (!p || Vector3.Distance(this.transform.position, p.transform.position) > 50)
        {
            alphaFactor = 0f;
        } else {
            alphaFactor = 1f;
        }

        KeyCode[] keyCodes = new[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyUp(keyCodes[i]))
            {
                selectedPreset = i;
            }
            
            SpriteRenderer sr = crosshairs[i].GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.4f * alphaFactor);

        }
        SpriteRenderer sr2 = crosshairs[selectedPreset].GetComponent<SpriteRenderer>();
        sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, 1f * alphaFactor);
        forceVector = forcePresets[selectedPreset];
    }

    public void ApplyForce()
    {
        //anim.enabled = false;
        GameObject stone = Instantiate(stonePrefab, stoneTarget.transform.position, stoneTarget.transform.rotation);

        stone.AddComponent<StoneCollision>();
        stone.GetComponent<StoneCollision>().particleSystemPrefab = particleSystemPrefab;
        stone.GetComponent<Rigidbody>().AddRelativeForce(forceVector.x, forceVector.y, forceVector.z);
        print("Force applied.");
    }
}
