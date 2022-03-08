using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileControll : MonoBehaviour
{
    public Sprite emptyTile;
    public Sprite selectedTile;

    private Text text;

    private bool isEditable = true;

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

    //set value of starting tiles
    public void SetStartingNumber(int num)
    {
        isEditable = false;
        if(num >0 && num < 9)
        {
            text.text = "" + num;
            isEditable = false;
        }
    }

    //getter for is editable variable
    public bool TileEditable()
    {
        return isEditable;
    }

}
