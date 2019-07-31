using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class MainPauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup is not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                hidePauseMenu();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showPauseMenu();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
                

        }
    }
    public void showPauseMenu()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f;
    }
    public void hidePauseMenu()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1f;
    }
}
