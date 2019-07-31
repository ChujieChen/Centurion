using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public ControllerInterface controller;
    public float rotateSpeed;
    public float moveSpeed;
    public Interactable focus;

    Animator anim;
    Rigidbody rbody;

    void Start()
    {

        anim = this.GetComponent<Animator>();
        rbody = this.GetComponent<Rigidbody>();

        //controller = gameObject.AddComponent<PlayerMovementController>();

        //if (controller == null)
        //{
        //    Debug.Log("No player controller");
        //}

    }
    void Update()
    {
        //anim.SetFloat("speedX", controller.getSpeedX());
        //anim.SetFloat("speedY", controller.getSpeedY());
        //anim.SetBool("attack", controller.getAttack());

        if (Input.GetMouseButtonDown(0)) // left click to interact (e.g. attack)
        {
            Ray ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = this.transform.forward;
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 1, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Removefocus();
        }

    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null) {
                focus.OnDefocused();
            }
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void Removefocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;

    }


    //void OnAnimatorMove()
    //{
    //    Vector3 newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z); 
    //    this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, moveSpeed);
    //    rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(controller.getSpeedX() * Time.deltaTime * rotateSpeed, Vector3.up));
    //}
}
