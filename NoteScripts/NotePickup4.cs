using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup4 : MonoBehaviour
{
    [SerializeField] Note note = null;

    [SerializeField] bool autoDisplay = false;
    [SerializeField] bool add = true;

    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.note5 == true)
            {
                NoteSystem.AddNote(note.Label, note);
                Destroy(gameObject);
            }
        }
    }
    public void Pickup()
    {
        if (autoDisplay)
        {
            NoteSystem.Display(note);
        }
        if (add)
        {
            NoteSystem.AddNote(note.Label, note);
            GameManager.instance.activeSave.note5 = true;
            Destroy(gameObject);
        }
    }
}
