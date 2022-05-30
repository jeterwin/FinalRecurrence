using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewObjectiveStart : MonoBehaviour
{
    private bool hasPlayed = false;
    public string On;
    public string text;
    public Animator animator;
    public Text objectiveText;
    public Text currentObjectiveText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasPlayed)
        {
            objectiveText.text = text;
            currentObjectiveText.text = text;
            animator.Play(On);
            hasPlayed = true;
        }
    }
}
