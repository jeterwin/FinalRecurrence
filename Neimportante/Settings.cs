using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Animator animator;
    public string goTo;
    public string backTo;

    public void GoTo()
    {
        animator.Play(goTo);
    }
    public void Back()
    {
        animator.Play(backTo);
    }
}
