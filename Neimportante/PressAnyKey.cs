using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private float elapsedTime;
    void Update()
    {
        //When the player presses any button start the fade out coroutine
        if(Input.anyKeyDown)
        {
            StartCoroutine(DoFadeOut());
        }
    }
    IEnumerator DoFadeOut()
    {
        //If the canvas group's alpha value is higher than 0 (visible on screen) keep reducing it until it finally becomes invisible
        //over a period of time
        //Also disable interactable and raycasts sso mouse clicks can go through the canvas
        while(canvasGroup.alpha > 0)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1.0f - (elapsedTime / 2));
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
     }
}
