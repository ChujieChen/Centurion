using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectedObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            Vector3 pos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                selectedObject = hit.collider.gameObject;

            }
        }
    }
}
