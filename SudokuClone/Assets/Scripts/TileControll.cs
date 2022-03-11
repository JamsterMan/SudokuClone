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

    public void SetPlayerValue(int value)
    {
        if (value > 0 && value < 10 && isEditable)
        {
            currentValue = value;
            //check if correction are on here, then change text colour
            //
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

}
