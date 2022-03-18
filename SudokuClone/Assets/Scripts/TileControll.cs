using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileControll : MonoBehaviour
{
    public Sprite normalTile;
    public Sprite selectedTile;
    public Sprite highlightTile;
    public Sprite sameAsSelectedTile;
    public Color playerInputCorrect;
    public Color playerInputIncorrect;

    private Text text;
    private NoteControl notes;

    private bool isEditable = true;
    private int correctValue;
    private int currentValue = 0;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        notes = GetComponentInChildren<NoteControl>();
        GetComponent<SpriteRenderer>().sprite = normalTile;
    }

    //sets tile to selected sprite
    public void SelectTile()
    {
        GetComponent<SpriteRenderer>().sprite = selectedTile;
    }

    //sets tile to normal tile sprite
    public void SetNormalTile()
    {
        GetComponent<SpriteRenderer>().sprite = normalTile;
    }

    //sets tile to highlighted sprite
    public void HighlightTile()
    {
        GetComponent<SpriteRenderer>().sprite = highlightTile;
    }

    //Sets tile to sameAsSelectedTile sprite
    public void HighlightSameNumberTile()
    {
        GetComponent<SpriteRenderer>().sprite = sameAsSelectedTile;
    }

    //set value of starting tiles
    public void SetStartingValue(int currentValue, int correctValue)
    {
        this.currentValue = currentValue;
        this.correctValue = correctValue;
        if(currentValue >0 && currentValue < 10)
        {
            text.text = "" + currentValue;
            isEditable = false;
        }
    }

    //sets value if player input it
    public void SetPlayerValue(int value, bool checkCorrectness)
    {
        if (value > 0 && value < 10 && isEditable)
        {
            currentValue = value;
            if (checkCorrectness)
            {
                if(currentValue == correctValue)
                {
                    text.color = playerInputCorrect;
                }
                else
                {
                    text.color = playerInputIncorrect;
                }
            }
            else
                text.color = playerInputCorrect;
            text.text = "" + value;
            RemoveAllNotes();// remove notes when value is added
        }
    }

    //getter for is editable variable
    public bool TileEditable()
    {
        return isEditable;
    }

    //returns if tile has the correct value
    public bool IsTileCorrect()
    {
        return currentValue == correctValue;
    }

    //changes text color to check for correctness
    public void StartCorrectnessCheck()
    {
        if (isEditable)
        {
            if (currentValue == correctValue)
            {
                text.color = playerInputCorrect;
            }
            else if( currentValue != 0)//empty tile check
            {
                text.color = playerInputIncorrect;
            }
        }
    }

    //changes text color to stop checking for correctness
    public void StopCorrectnessCheck()
    {
        if (isEditable)
        {
            text.color = playerInputCorrect;
        }
    }

    //resets the Tile to creation values
    public void ResetTile()
    {
        isEditable = true;
        currentValue = 0;
        correctValue = 0;
        text.text = "";
    }

    //remove the added value
    public void RemoveValue()
    {
        if (isEditable)
        {
            currentValue = 0;
            text.text = "";
        }
    }

    //removes all notes
    private void RemoveAllNotes()
    {
        notes.RemoveNotes();
    }

    //show the note corrisponding to val (1 to 9)
    public void SetNoteValue(int val)
    {
        if (isEditable && currentValue == 0)
            notes.ShowNoteValue(val);
    }

    //currentValue gettter
    public int GetCurrentValue()
    {
        return currentValue;
    }

    //for testing**********************************************************
    public void CompleteTile()
    {
        if (isEditable)
        {
            currentValue = correctValue;
            text.color = playerInputCorrect;
            text.text = "" + currentValue;
        }
    }
}
