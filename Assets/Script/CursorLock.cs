using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public static bool cursorLocked;
    //private bool StopUpdate=false;

    void Start()
    {
        cursorLocked = true;
        UpdateCursorLock();
    }

    
    //void Update()
    //{
    //    if(!StopUpdate)
    //       UpdateCursorLock();
    //}

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    cursorLocked = false;
            //}
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    cursorLocked = true;
            //}
        }
    }

    public void CursorVisibleSetup(bool permission)
    {
        cursorLocked = permission;
        UpdateCursorLock();
    }
}
