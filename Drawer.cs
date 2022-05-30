using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Animator animator;
    public string OpenAnimation;
    public string CloseAnimation;
    private bool isOpened = false;
    public bool canInteract = true;
    public void OpenDrawer()
    {
        if(isOpened == false && canInteract == true)
        {
            animator.Play(OpenAnimation);
            isOpened = !isOpened;
        }
        else if(isOpened == true && canInteract == true)
        {
            animator.Play(CloseAnimation);
            isOpened = !isOpened;
        }
    }

}
