using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoard : MonoBehaviour
{
    private TileControl[,] tileGrid = new TileControl[9,9];
    private int xSelect = 0,ySelect = 8;
    private readonly int boardSize = 9;
    private UndoScript undoControl = new UndoScript();

    // Start is called before the first frame update
    void Awake()
    {
        GenerateGrid();
        HighlightImportantTiles();
        tileGrid[xSelect, ySelect].SelectTile();//0,8 so that selection starts at the top left corner
    }

    public void ResetTiles()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                tileGrid[x, y].ResetTile();
            }
        }
    }

    //Generates the 9x9 grid on tiles (spaces added to show the square regions)
    private void GenerateGrid()
    {
        float xOff, yOff;
        for (int x = 0; x < boardSize; x++)
        {
            if (x < 3)
                xOff = 0f;
            else if (x < 6)
                xOff = 0.25f;
            else
                xOff = 0.5f;

            for (int y = 0; y < boardSize; y++)
            {
                if (y < 3)
                    yOff = 0f;
                else if (y < 6)
                    yOff = 0.25f;
                else
                    yOff = 0.5f;

                GameObject gameObject = Instantiate(Resources.Load("Prefabs/Tile", typeof(GameObject)), new Vector3(x + xOff,  y + yOff, 0), Quaternion.identity) as GameObject;
                gameObject.transform.parent = this.transform;//keeps unity editor clean
                tileGrid[x, y] = gameObject.GetComponent<TileControl>();
            }
        }
    }

    //move selection to the left if possible
    public void TileSelectionLeft()
    {
        if (xSelect > 0)
        {
            TileUnhighlighting();
            xSelect--;
            TileHighlighting();
        }
    }
    //move selection to the right if possible
    public void TileSelectionRight()
    {
        if (xSelect < boardSize - 1)
        {
            TileUnhighlighting();
            xSelect++;
            TileHighlighting();
        }
    }
    //move selection to the up if possible
    public void TileSelectionUp()
    {
        if (ySelect < boardSize - 1)
        {
            TileUnhighlighting();
            ySelect++;
            TileHighlighting();
        }
    }
    //move selection to the down if possible
    public void TileSelectionDown()
    {
        if (ySelect > 0)
        {
            TileUnhighlighting();
            ySelect--;
            TileHighlighting();
        }
    }

    //highlights tiles involving selceted tile
    //eg. row, column, square, and same number value tiles
    private void TileHighlighting()
    {
        if (tileGrid[xSelect, ySelect].GetCurrentValue() != 0) {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (tileGrid[xSelect, ySelect].GetCurrentValue() == tileGrid[x, y].GetCurrentValue())
                    {
                        tileGrid[x, y].HighlightSameNumberTile();
                    }
                }
            }
        }
        HighlightImportantTiles();
        HighlightTileMatches();

        tileGrid[xSelect, ySelect].SelectTile();
    }

    //Unhighlights all tiles
    private void TileUnhighlighting()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                tileGrid[x, y].SetNormalTile();
            }
        }
    }

    //highlights every tile that cant have the same number as the selected tile
    //eg. whole row, whole column, square containing selected tile
    private void HighlightImportantTiles()
    {
        int xOff, yOff,x,y;
        if (xSelect < 3)
            xOff = 0;
        else if (xSelect < 6)
            xOff = 3;
        else
            xOff = 6;
        if (ySelect < 3)
            yOff = 0;
        else if (ySelect < 6)
            yOff = 3;
        else
            yOff = 6;
        for (x = xOff; x < xOff +3; x++)//highlight square containing selected tile
        {
            for (y = yOff; y < yOff + 3; y++)
            {
                tileGrid[x, y].HighlightTile();
            }
        }
        for (x = 0; x < boardSize; x++)//highlight row
        {
            tileGrid[x, ySelect].HighlightTile();
        }
        for (y = 0; y < boardSize; y++)//highlight column
        {
            tileGrid[xSelect, y].HighlightTile();
        }

    }

    //highlights if two tiles that can see each other have the same value
    private void HighlightTileMatches()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                HighlightImportantTileMatches(x, y);
            }
        }

    }

    //highlights tile matchs for tile (xSel, ySel)
    private void HighlightImportantTileMatches(int xSel, int ySel)
    {
        if (tileGrid[xSel, ySel].GetCurrentValue() != 0)//Dont check if the tile at xSel,ySel has no value(eg. 0)
        {
            int xOff, yOff, x, y;
            if (xSel < 3)
                xOff = 0;
            else if (xSel < 6)
                xOff = 3;
            else
                xOff = 6;
            if (ySel < 3)
                yOff = 0;
            else if (ySel < 6)
                yOff = 3;
            else
                yOff = 6;
            for (x = xOff; x < xOff + 3; x++)//highlight square containing selected tile
            {
                for (y = yOff; y < yOff + 3; y++)
                {
                    if (tileGrid[xSel, ySel].GetCurrentValue() == tileGrid[x, y].GetCurrentValue() && xSel != x && ySel != y)
                        tileGrid[x, y].SetIncorrectTile();
                }
            }
            for (x = 0; x < boardSize; x++)//highlight row
            {
                if (tileGrid[xSel, ySel].GetCurrentValue() == tileGrid[x, ySel].GetCurrentValue() && xSel != x)
                    tileGrid[x, ySel].SetIncorrectTile();
            }
            for (y = 0; y < boardSize; y++)//highlight column
            {
                if (tileGrid[xSel, ySel].GetCurrentValue() == tileGrid[xSel, y].GetCurrentValue() && ySel != y)
                    tileGrid[xSel, y].SetIncorrectTile();
            }
        }
    }

    //adds value to tile that is currently selected
    public void AddTileValue(int value, bool checkCorrectness)
    {
        undoControl.AddCommand(UndoValues());
        tileGrid[xSelect, ySelect].SetPlayerValue(value, checkCorrectness);
        TileUnhighlighting();
        TileHighlighting();
    }

    //remove value in selected tile
    public void RemoveTileValue()
    {
        tileGrid[xSelect, ySelect].RemoveValue();
        TileUnhighlighting();
        TileHighlighting();
    }

    //adds val as a note (not counted in correctness/completion)
    public void AddNoteValue(int value)
    {
        undoControl.AddCommand(UndoValues());
        tileGrid[xSelect, ySelect].SetNoteValue(value);
    }
    
    //sets tiles to starting values
    //correct value is the value when the board is filled
    //current value is the value after removing numbers
    public void SetTileBoard(int x, int y, int currentValue, int correctValue)
    {
        tileGrid[x, y].SetStartingValue(currentValue, correctValue);
    }

    //checks if the board is correct and completely filled
    public bool IsBoardCorrect()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (tileGrid[x, y].IsTileCorrect() == false)
                    return false;
            }
        }
        return true;
    }

    //sets tiles to the correct text color 
    public void SetBoardCorrectness(bool checkCorrectness)
    {
        if (checkCorrectness)
        {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    tileGrid[x, y].StartCorrectnessCheck();
                }
            }
        }
        else
        {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    tileGrid[x, y].StopCorrectnessCheck();
                }
            }
        }
    }

    //for testing
    public void CompleteBoardTiles()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                tileGrid[x, y].CompleteTile();
            }
        }
    }

    //sets up the struct used to store the state before an action
    private UndoTiles UndoValues()
    {
        UndoTiles undo = new UndoTiles();
        undo.row = xSelect;
        undo.col = ySelect;
        undo.oldValue = tileGrid[xSelect, ySelect].GetCurrentValue();
        undo.undoNotes = new bool[9];
        for (int i = 0; i < 9; i++)
        {
            undo.undoNotes[i] = tileGrid[xSelect, ySelect].IsNoteActive(i + 1);
        }
        return undo;
    }

    public void UndoTiles(bool checkCorrectness)
    {
        UndoTiles undo = undoControl.GetLastCommand();
        if (undo.row != -1 && undo.col != -1)
        {
            tileGrid[undo.row, undo.col].SetUndoValue(undo.oldValue, checkCorrectness);
            for (int i = 0; i < 9; i++)
            {
                tileGrid[undo.row, undo.col].SetUndoNoteValue(i + 1, undo.undoNotes[i]);
            }
            TileUnhighlighting();
            TileHighlighting();
        }
        else
        {
            Debug.Log("No commands Left to Undo");
        }
    }
}
