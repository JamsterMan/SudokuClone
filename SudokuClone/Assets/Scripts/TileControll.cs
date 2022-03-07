using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControll : MonoBehaviour
{
    public Sprite emptyTile;
    public Sprite selectedTile;

    public void SelectTile()
    {
        GetComponent<SpriteRenderer>().sprite = selectedTile;
    }

    public void DeSelectTile()
    {
        GetComponent<SpriteRenderer>().sprite = emptyTile;
    }
}
