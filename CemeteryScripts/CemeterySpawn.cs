using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CemeterySpawn : MonoBehaviour
{
    public GameObject[] Person;
    public int PersonActive = 0;
    public int TimeToEnable = 5;
    public GameObject Image;
    public AudioSource HeartBeat;
    public GameObject letter;
    public UnityEvent @event;
    IEnumerator EnablePerson()
    {
        Hello();
        yield return new WaitForSeconds(TimeToEnable);
        StartCoroutine(EnablePerson());
    }
    public void Hello()
    {
        if(PersonActive <= 9)
        Person[PersonActive].SetActive(true);
        PersonActive += 1;

    }
    public void StartEnable()
    {
        StartCoroutine(EnablePerson());

    }
    public void CheckPerson()
    {
        bool IsEnable = false;
        for (int i = 0; i < Person.Length; i++)
            if (Person[i].active == true)
            {
                IsEnable = true;
            }
        if (IsEnable == false)
        {
            Image.SetActive(false);
            HeartBeat.Stop();
            //letter.SetActive(true);
            @event.Invoke();
        }
    }

}
