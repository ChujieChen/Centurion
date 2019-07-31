using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroyEvent : MonoBehaviour
{
    [System.Obsolete]
    void Start()
    {
        var ps = this.GetComponentsInChildren<ParticleSystem>();
        float duration = 0f;

        foreach(var p in ps)
        {
            if (p.duration > duration)
                duration = p.duration;
        }
        Destroy(this.gameObject, duration);
    }
}
