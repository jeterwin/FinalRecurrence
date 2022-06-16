using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    public PauseMenu menu;
    public void EnableCursorPuzzel()
    {
        menu.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void DisableCursorPuzzel()
    {
        menu.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
