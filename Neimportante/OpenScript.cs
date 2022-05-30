using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScript : MonoBehaviour
{
    public Animator animator;
    public string open;
    public string close;

    public void Open()
    {
        animator.Play(open);
    }
    public void Close()
    {
        animator.Play(close);
    }
}
