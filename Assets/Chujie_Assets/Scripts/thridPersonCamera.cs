using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @author: Chujie Chen
 * @Last edit: 06/12/2019
 * Code is based on youtube video:
 * https://www.youtube.com/watch?v=Ta7v27yySKs
 * This camera controller cannot only give us a thirdPersonView, but we also can
 * use the mouse to navigate where we are looking at. Parameters like X_ANGLE_MIN
 * are used to confine the view range.
 */

public class thridPersonCamera : MonoBehaviour
{
    // left and right, 90 degrees
    private const float X_ANGLE_MIN = -90.0f;
    private const float X_ANGLE_MAX = 90.0f;
    // up 50 degrees, down 20 degrees
    private const float Y_ANGLE_MIN = 20.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // show mouse cursor
        Cursor.visible = true;
        camTransform = transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += sensitivityX * Input.GetAxis("Mouse X");
        // inverse Y axis
        currentY -= sensitivityY * Input.GetAxis("Mouse Y");

        currentX = Mathf.Clamp(currentX, X_ANGLE_MIN, X_ANGLE_MAX);
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        if (!lookAt)
            return;
        // a displacement from the camera to the character
        Vector3 dir = new Vector3(0, 0, -distance);
        Vector3 forward = lookAt.forward * -2;
        Vector3 overShoulder = new Vector3(0, 2f, 0);
        Vector3 finalVec = forward + overShoulder;
        //Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        // instead of "adding" rotations. We need to multiply them.
        Quaternion rotation = lookAt.rotation * Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir+ overShoulder + forward;
        camTransform.LookAt(lookAt.position + overShoulder);
    }
}
