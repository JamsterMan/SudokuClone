using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoard : MonoBehaviour
{
    private TileControll[,] tileGrid = new TileControll[9,9];
    private int xSelect = 0,ySelect = 8;
    private readonly int boardSize = 9;

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
                tileGrid[x, y] = gameObject.GetComponent<TileControll>();
            }
        }
    }

    //move selection to the left if possible
    public void TileSelectionLeft()
    {
        if (xSelect > 0)
        {
            UnHighlightImportantTiles();
            tileGrid[xSelect, ySelect].DeSelectTile();
            xSelect--;
            HighlightImportantTiles();
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }
    //move selection to the right if possible
    public void TileSelectionRight()
    {
        if (xSelect < boardSize - 1)
        {
            UnHighlightImportantTiles();
            tileGrid[xSelect, ySelect].DeSelectTile();
            xSelect++;
            HighlightImportantTiles();
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }
    //move selection to the up if possible
    public void TileSelectionUp()
    {
        if (ySelect < boardSize - 1)
        {
            UnHighlightImportantTiles();
            tileGrid[xSelect, ySelect].DeSelectTile();
            ySelect++;
            HighlightImportantTiles();
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }
    //move selection to the down if possible
    public void TileSelectionDown()
    {
        if (ySelect > 0)
        {
            UnHighlightImportantTiles();
            tileGrid[xSelect, ySelect].DeSelectTile();
            ySelect--;
            HighlightImportantTiles();
            tileGrid[xSelect, ySelect].SelectTile();
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

    private void UnHighlightImportantTiles()
    {
        int xOff, yOff, x, y;
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
        for (x = xOff; x < xOff + 3; x++)//highlight square containing selected tile
        {
            for (y = yOff; y < yOff + 3; y++)
            {
                tileGrid[x, y].UnHighlightTile();
            }
        }
        for (x = 0; x < boardSize; x++)//highlight row
        {
            tileGrid[x, ySelect].UnHighlightTile();
        }
        for (y = 0; y < boardSize; y++)//highlight column
        {
            tileGrid[xSelect, y].UnHighlightTile();
        }

    }

    //adds value to tile that is currently selected
    public void AddTileValue(int value, bool checkCorrectness)
    {
        tileGrid[xSelect, ySelect].SetPlayerValue(value, checkCorrectness);
    }

    //remove value in selected tile
    public void RemoveTileValue()
    {
        tileGrid[xSelect, ySelect].RemoveValue();
    }

    //adds val as a note (not counted in correctness/completion)
    public void AddNoteValue(int value)
    {
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
}
