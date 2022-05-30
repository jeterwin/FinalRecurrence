using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private float elapsedTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            StartCoroutine(DoFadeOut());
        }
    }
    IEnumerator DoFadeOut()
    {
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
