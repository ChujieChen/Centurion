using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour
{
    public float rotationSpeed = 40; // degrees per second

    Animator animator;

    public SelectionManager selectionManager;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (selectionManager.selectedObject != gameObject)
        //    return;
        //Edited by Yueqing Zhang
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (!p)
            return;
        if (Vector3.Distance(this.transform.position, p.transform.position) > 50)
            return;


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetTrigger("Throw");
        }
        else
        {
            animator.ResetTrigger("Throw");
        }


        float v = Input.GetAxis("Horizontal");
        if (v < 0)
        {
            // Rotating left
            animator.SetTrigger("RotateLeft");
            animator.ResetTrigger("Idle");
        }
        else if (v > 0)
        {
            // Rotating right
            animator.SetTrigger("RotateRight");
            animator.ResetTrigger("Idle");
        }
        else
        {
            animator.ResetTrigger("RotateRight");
            animator.ResetTrigger("RotateLeft");
            animator.SetTrigger("Idle");
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * v);
    }
}
