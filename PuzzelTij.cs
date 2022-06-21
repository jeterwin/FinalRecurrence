using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PuzzelTij : MonoBehaviour
{
    public bool[] TijStatus;
    public UnityEvent @event;
    public void SetActiveTij(int i)
    {
        TijStatus[i] = true;
    }
    public void SetActiveFalseTij(int i)
    {
        TijStatus[i] = false;
    }
    public void FinalCheck()
    {
        int ok = 1;
        for (int i = 0; i < TijStatus.Length; i++)
        {

            if (TijStatus[i] == false)
            {
                ok = 0;
            }

        }
        if (ok == 1)
        {
            @event.Invoke();
        }

    }
    private void Update()
    {
        FinalCheck();


    }

}
