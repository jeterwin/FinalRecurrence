using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup10 : MonoBehaviour
{
    [SerializeField] Note note = null;

    [SerializeField] bool autoDisplay = false;
    [SerializeField] bool add = true;

    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.note11 == true)
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
            GameManager.instance.activeSave.note11 = true;
            Destroy(gameObject);
        }
    }
}
