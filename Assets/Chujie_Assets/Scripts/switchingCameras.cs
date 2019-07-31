using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchingCameras : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera spectatorCamera;

    public void ShowThirdPersonView()
    {
        spectatorCamera.enabled = false;
        spectatorCamera.GetComponent<AudioListener>().enabled = false;
        thirdPersonCamera.enabled = true;
        thirdPersonCamera.GetComponent<AudioListener>().enabled = true;
    }

    public void ShowSpectatorView()
    {
        spectatorCamera.enabled = true;
        spectatorCamera.GetComponent<AudioListener>().enabled = true;
        thirdPersonCamera.enabled = false;
        thirdPersonCamera.GetComponent<AudioListener>().enabled = false;
    }
    void Start()
    {
        ShowSpectatorView();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (spectatorCamera.enabled)
            {
                ShowThirdPersonView();
            }
            else
            {
                ShowSpectatorView();
            }
        }
    }
}
