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
    public bool canRun = true;

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
        //If the player has any stamina left in him and the player is not crouching, can move and the sprint button
        //is hold, call the useStamina function and play the animator (the stamina bar fades in slowly on the screen)
        //and change the player's walking speed to the sprint speed

        //If the player releases the sprint button then change the player's speed to the normal speed

        //If the stamina hits 0 or below, change the moving speed to normal and start the coroutine to gain stamina
        if (currentStamina > 0)
        {
            if (Input.GetKey(GameManager.instance.shift) && characterController.isGrounded && Fps_Script.instance.isCrouching == false
                && Fps_Script.instance.canMove == true && Fps_Script.instance.moveDirection.x != 0 && canRun == true)
            {
                UseStamina(0.25f);
                if (canPlay == true)
                    animator.Play(fadeIn);
                player.walkingSpeed = normalSprint;
                player.isRunning = true;
            }
            else
            {
                player.walkingSpeed = normalWalk;
                player.isRunning = false;
            }
            if (Input.GetKeyUp(GameManager.instance.shift))
            {
                player.walkingSpeed = normalWalk;
                player.isRunning = false;
            }
        }
        //Se pot schimba valorile pt sprint
        else
        {
            player.walkingSpeed = normalWalk;
            player.isRunning = false;
        }
        if (currentStamina >= 99)
            canPlay = true;
        staminaSlider.value = currentStamina;
    }
    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= -0.25)
        {
            //Reeduce the stamina and update the slider with the current stamina left
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
        //After waiting for x seconds, start regaining stamina and fade out the stamina bar off the player's screen
        yield return new WaitForSeconds(1.40f);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 60;
            staminaSlider.value = currentStamina;
            animator.Play(fadeOut);
            canPlay = true;
            yield return regenTick;
        }
    }
    public void ChangeSpeed(float newWalk)
    {
        normalWalk = newWalk;
        player.walkingSpeed = newWalk;
    }
    public void StopSprint()
    {
        normalSprint = 0;
        player.runningSpeed = 0;
        staminaSlider.enabled = false;

    }
}
