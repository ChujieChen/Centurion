using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerStat : CharacterStats
{
    public GameObject DestroyedSpawner;
    public float shakeIntensity = .05f;
    private float remainShakeTime = 0f;
    private Vector3 originPosition;
    private Quaternion originRotation;

    protected override void Start()
    {
        base.Start();
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    public override void getDamage(int damage, bool isEnv = true)
    {
        int thisDamage = damage - armor;
        Mathf.Clamp(thisDamage, 0, int.MaxValue);
        currentHealth -= thisDamage;

        if (currentHealth <= 0)
        {
            Instantiate(DestroyedSpawner, transform.position, transform.rotation);
            Destroy(this.transform.gameObject);
        }
        remainShakeTime = 1000;
    }
    private void Update()
    {
        if(remainShakeTime >= 0)
        {
            float myShakeIntensity = Mathf.Min(remainShakeTime / 1000, shakeIntensity);
            remainShakeTime -= Time.deltaTime * 1000;
            Vector3 newPos = originPosition + Random.insideUnitSphere * myShakeIntensity;
            newPos.y = originPosition.y;
            transform.position = newPos;
            transform.rotation = new Quaternion
            (
                originRotation.x,
                originRotation.y + Random.Range(-myShakeIntensity, myShakeIntensity) * .2f,
                originRotation.z,
                originRotation.w
            );
        }
        else
        {
            remainShakeTime = 0;
            transform.position = originPosition;
            transform.rotation = originRotation;
        }
    }

}
