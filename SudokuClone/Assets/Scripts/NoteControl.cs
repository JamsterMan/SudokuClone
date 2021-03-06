using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteControl : MonoBehaviour
{
    private Text[] texts = new Text[9];

    private void Start()
    {
        texts = GetComponentsInChildren<Text>();
        RemoveNotes();
    }

    //sets text in val -1 index to active/inactive, and returns true if activated/ false if deactivated
    public bool ShowNoteValue(int val)
    {
        texts[val -1].gameObject.SetActive(!texts[val-1].IsActive());
        return texts[val - 1].IsActive();
    }

    //hides all the notes
    public void RemoveNotes()
    {
        foreach (Text text in texts)
        {
            text.gameObject.SetActive(false);
        }
    }

    //sets notes to active (used for undo functionality)
    public void SetNoteValue(int val, bool setValue)
    {
        texts[val - 1].gameObject.SetActive(setValue);
    }
}
