using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class pickCamera : MonoBehaviour
{
    public float maxMoveSpeed;
    public float accelerationMove;

    public float maxUpDownSpeed;
    public float accelerationUpDown;


    float speedX = 0;
    float speedY = 0;
    float speedZ = 0;
    float x      = 0;
    float y      = 0;
    bool  goDown = false;
    bool  goUp   = false;
    Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        direction = this.transform.forward.normalized;

        //menu fix
        Time.timeScale = 0f;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame
    void Update()
    {
        keyUpdate();
        Vector3 verticalVector = Vector3.up;
        Vector3 rightVector = Vector3.Cross(direction, verticalVector).normalized;
        Vector3 frontVector = Vector3.Cross(rightVector, Vector3.up).normalized;
        rightVector *= speedX;
        if (x > 0)
            rightVector = -rightVector;
        frontVector *= speedZ;
        if (y > 0)
            frontVector = -frontVector;
        verticalVector *= speedY;
        if (goDown)
            verticalVector *= -1;
        this.transform.position += rightVector;
        this.transform.position += verticalVector;
        this.transform.position += frontVector;
    }
    void keyUpdate()
    {
        x      = Input.GetAxis("Horizontal");
        y      = Input.GetAxis("Vertical");
        goUp   = Input.GetKey(KeyCode.E);
        goDown = Input.GetKey(KeyCode.Q);
        if (System.Math.Abs(x) > float.Epsilon)
            speedX = Mathf.Lerp(speedX, maxMoveSpeed, accelerationMove);
        else
            speedX = 0;

        if (System.Math.Abs(y) > float.Epsilon)
            speedZ = Mathf.Lerp(speedZ, maxMoveSpeed, accelerationMove);
        else
            speedZ = 0;

        if (goUp || goDown)
            speedY = Mathf.Lerp(speedY, maxUpDownSpeed, accelerationUpDown);
        else
            speedY = 0;
    }
}
