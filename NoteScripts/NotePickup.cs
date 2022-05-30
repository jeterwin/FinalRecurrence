using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [SerializeField] Note note = null;

    [SerializeField] bool autoDisplay = false;
    [SerializeField] bool add = true;

    private void Start()
    {
        if(SaveManager.instance.hasLoaded)
        {
            if(SaveManager.instance.activeSave.note1 == true)
            {
                NoteSystem.AddNote(note.Label, note);
                Destroy(gameObject);
            }
        }
    }
    public void Pickup()
    {
        if(autoDisplay)
        {
            NoteSystem.Display(note);
        }
        if(add)
        {
            NoteSystem.AddNote(note.Label, note);
            SaveManager.instance.activeSave.note1 = true;
            Destroy(gameObject);
        }
        SaveManager.instance.activeSave.canOpenInventory = true;
        //SaveManager.instance.Save(); Pe cand o fi lansat jocu
    }
}
