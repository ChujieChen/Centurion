using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineMover : MonoBehaviour
{
    // Start is called before the first frame update
    float z;
    void Start()
    {
        z = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = this.transform.position;
        float time = Time.time % 20;
        newPos.z = z + (float)(time * 0.5 - 5f);
        this.transform.position = newPos;
    }
}
