using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRolling : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (this.transform.position - other.transform.position).normalized;
            GetComponent<Rigidbody>().velocity = speed * direction;
            sceneController.simpleAddHint("boulder");
        }
    }
}
