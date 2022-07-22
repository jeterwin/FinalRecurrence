using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChangeEnding : MonoBehaviour
{
    public int GoodEnding;
    public int BadEnding;
    public UnityEvent GoodEndingEvents;
    public UnityEvent BadEndingEvents;
    public void GoodEndingIncrease()
    {
        GoodEnding++;
        GameManager.instance.activeSave.goodEndings++;
    }
    public void CheckEndingStatus()
    {
        GoodEnding = GameManager.instance.activeSave.goodEndings; //Or SaveManager
        BadEnding = 5 - GoodEnding;
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
