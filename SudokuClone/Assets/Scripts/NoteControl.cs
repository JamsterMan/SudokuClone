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

    //sets text in val -1 index to active/inactive
    public void ShowNoteValue(int val)
    {
        texts[val -1].gameObject.SetActive(!texts[val-1].IsActive());
    }

    public void RemoveNotes()
    {
        foreach (Text text in texts)
        {
            text.gameObject.SetActive(false);
        }
    }
}
