using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChangeEnding : MonoBehaviour
{
    public int GoodEnding;
    public int BadEnding = 5;
    public UnityEvent GoodEndingEvents;
    public UnityEvent BadEndingEvents;
    public void GoodEndingIncrease()
    {
        GoodEnding++;
        BadEnding--;
        PlayerPrefs.SetInt("Good", GoodEnding);
        PlayerPrefs.SetInt("Bad", BadEnding);
    }
    public void CheckEndingStatus()
    {
        GoodEnding = PlayerPrefs.GetInt("Good");
        BadEnding = PlayerPrefs.GetInt("Bad");
        if (GoodEnding > BadEnding)
        {
            GoodEndingEvents.Invoke();
        }
        else
        {
            BadEndingEvents.Invoke();
        }
    }
}
