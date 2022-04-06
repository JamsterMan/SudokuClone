using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct used for undo functionallity
public struct UndoTiles
{
    public int row, col;
    public int oldValue;
    public bool[] undoNotes;// = new bool[9];
}