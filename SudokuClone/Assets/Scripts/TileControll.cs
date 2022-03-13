using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileControll : MonoBehaviour
{
    public Sprite emptyTile;
    public Sprite selectedTile;
    public Sprite highlightTile;
    public Color playerInputCorrect;
    public Color playerInputIncorrect;

    private Text text;

    private bool isEditable = true;
    private int correctValue;
    private int currentValue = 0;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void SelectTile()
    {
        GetComponent<SpriteRenderer>().sprite = selectedTile;
    }

    public void DeSelectTile()
    {
        GetComponent<SpriteRenderer>().sprite = emptyTile;
    }

    public void UnHighlightTile()
    {
        GetComponent<SpriteRenderer>().sprite = emptyTile;
    }

    public void HighlightTile()
    {
        GetComponent<SpriteRenderer>().sprite = highlightTile;
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
            //check if correction are on here, then change text colour
            //
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

    //for testing
    public void CompleteTile()
    {
        if (isEditable)
        {
            currentValue = correctValue;
            text.color = playerInputCorrect;
            text.text = "" + currentValue;
        }
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

    public void ResetTile()
    {
        isEditable = true;
        currentValue = 0;
        correctValue = 0;
        text.text = "";
    }

}
