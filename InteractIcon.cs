using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
/// <summary>
/// WORLD SPACE CANVAS FACES PLAYER AT ALL TIMES
/// </summary>
public class InteractIcon : MonoBehaviour
{
    public Camera m_Camera;
    public float sightRange;
    public LayerMask whatIsPlayer;
    [HideInInspector]
    public bool playerInSightRange;
    public Image Icon;
    public string AnimationOnScreen;
    public string AnimationOffScreen;
    [SerializeField] private Animator Icon_Animator = null;

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (playerInSightRange)
        {
            //Icon.enabled = true;
            if(Icon_Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationOnScreen) == false)
            OnScreen();
        }
        else
        {
            //Icon.enabled = false;
            if(Icon_Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationOffScreen) == false)
            OffScreen();
        }
        //transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
 Vector3 targetPostition = new Vector3( m_Camera.transform.position.x, 
                                        this.transform.position.y, 
                                        m_Camera.transform.position.z ) ;
 this.transform.LookAt( targetPostition ) ;

    }
    public void OnScreen()
    {
        Icon_Animator.Play(AnimationOnScreen, 0, 0.0f);
       
    }
    public void OffScreen()
    {
        Icon_Animator.Play(AnimationOffScreen, 0, 0.0f);
    }
}