using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class UndoTiles
{
    private int row, col;
    private int oldValue;
    private bool[] UndoNotes = new bool[9];
}*/

public struct UndoTiles
{
    public int row, col;
    public int oldValue;
    public bool[] undoNotes;// = new bool[9];
}