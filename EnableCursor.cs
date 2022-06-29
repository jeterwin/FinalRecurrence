using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCursor : MonoBehaviour
{   //Variables
    public PauseMenu menu;
    //This function helps us to activate the cursor, with the help of a command created by unity
    public void EnableCursorPuzzel()
    {
        menu.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    //This function helps us to dezactivate the cursor, with the help of a command created by unity
    public void DisableCursorPuzzel()
    {
        menu.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
