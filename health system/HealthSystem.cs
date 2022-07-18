using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
public class HealthSystem : MonoBehaviour
{
    #region Variables
    public static HealthSystem instance;
    public TextMeshProUGUI pillAmount;
    public float sanityValue, ValueAdd, newVal, maxHealthValue;
    public int DrugsAmount;
    public Image IconImage;
    public UnityEvent IfValue0_Event;
    public UnityEvent IfValue1_Event;
    public PostProcessProfile newVolume;
    private ChromaticAberration chromaticAberration;
    public Animator animator;
    private bool dangerouslyLow;
    public AudioSource takePillSound;
    #endregion 
    public void Start()
    {
        IconImage.fillAmount = sanityValue;
        //If the player reached the point where sanity is unlocked, change the sanity pill amount to the last
        //amount of pills they had, otherwise leave it at none
        if (SaveManager.instance.activeSave.hasReachedSanity == true)
            pillAmount.text = SaveManager.instance.activeSave.medicines + "x";
        chromaticAberration = newVolume.GetSetting<ChromaticAberration>();
    }
    private void Awake()
    {
        instance = this;
        //chromaticAberration = newVolume.profile.GetSetting<ChromaticAberration>();
    }
    public void Jumpscare(float minusSanity)
    {
        if(sanityValue - minusSanity < 0)
            sanityValue = 0;
        else
            sanityValue -= minusSanity;
        if (animator != null)
            animator.Play("minusSanity");
    }
    public void SolvedPuzzle(float plusSanity)
    {
        if (sanityValue + plusSanity > 100)
            sanityValue = 100;
        else
            sanityValue += plusSanity;
        if (animator != null)
            animator.Play("minusSanity");
    }
    public void Update()
    {
        //If the player presses the H key, they reached the point where they can use sanity pills and they have any sanity
        //pills left to use, consume one, add the sanity and reduce it's quantity by one
        if (Input.GetKey(KeyCode.H) && DrugsAmount > 0 && SaveManager.instance.activeSave.hasReachedSanity == true)
        {
            sanityValue += ValueAdd;
            if (sanityValue + ValueAdd > 100)
                sanityValue = 100;
            DrugsAmount -= 1;
            pillAmount.text = DrugsAmount + "x";
            takePillSound.Play();
        }
        IconImage.fillAmount = sanityValue / 100;
        if (sanityValue < 25)
        {
            newVal = Random.Range(0.1f, 1);
            Stamina.instance.canRun = false;
            dangerouslyLow = true;
            chromaticAberration.intensity.value = newVal;
            IfValue0_Event.Invoke();
        }
        else
        {
            IfValue1_Event.Invoke();
            Stamina.instance.canRun = true;
            dangerouslyLow = false;
            chromaticAberration.intensity.value = 0;
        }
    }

    public void PickUpDrugs()
    {
        DrugsAmount += 1;
    }
}
