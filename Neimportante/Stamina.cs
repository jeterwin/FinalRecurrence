using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public CharacterController characterController;
    private int maxStamina = 100;
    private float currentStamina;
    public float normalSprint;
    public float normalWalk;
    public Slider staminaSlider;
    public Animator animator;
    public string fadeIn;
    public string fadeOut;
    private bool canPlay = true;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static Stamina instance;

    public Fps_Script player;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Se pot schimba valorile pt sprint
        if(currentStamina > 0)
        {
            if(Input.GetKey(GameManager.instance.shift) && characterController.isGrounded && Fps_Script.instance.isCrouching == false && Fps_Script.instance.canMove == true && Fps_Script.instance.moveDirection.x != 0)
            {
                UseStamina(0.25f);
                if (canPlay == true)
                    animator.Play(fadeIn);
                player.walkingSpeed = normalSprint;
                player.isRunning = true;
                //FootSteps.instance.animator.SetBool("isRunning", true);
            }
            if(Input.GetKeyUp(GameManager.instance.shift))
            {
                player.walkingSpeed = normalWalk;
                player.isRunning = false;
                //FootSteps.instance.animator.SetBool("isRunning", false);
            }
        }
        //Se pot schimba valorile pt sprint
        else
        {
            player.walkingSpeed = normalWalk;
            player.isRunning = false;
            //FootSteps.instance.animator.SetBool("isRunning", false);
        }
        if (currentStamina >= 99)
            canPlay = true;
        staminaSlider.value = currentStamina;
    }
    public void UseStamina(float amount)
    {
        if(currentStamina - amount >= -0.25)
        {
            currentStamina -= amount;
            staminaSlider.value = currentStamina;
            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaSlider.value = currentStamina;
            animator.Play(fadeOut);
            canPlay = true;
            yield return regenTick;
        }
    }
}
