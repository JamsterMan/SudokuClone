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
        tileGrid[0, 8].SelectTile();//0,8 so that selection starts at the top left corner
    }

    //Generates the 9x9 grid on tiles (spaces added to show the square regions)
    private void GenerateGrid()
    {
        float xOff, yOff;
        for (int x = 0; x < 9; x++)
        {
            if (x < 3)
                xOff = 0f;
            else if (x < 6)
                xOff = 0.25f;
            else
                xOff = 0.5f;

            for (int y = 0; y < 9; y++)
            {
                if (y < 3)
                    yOff = 0f;
                else if (y < 6)
                    yOff = 0.25f;
                else
                    yOff = 0.5f;
                //tileGrid[x,y] = Instantiate(Resources.Load("Prefabs/Tile", typeof(GameObject)), new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                GameObject gameObject = Instantiate(Resources.Load("Prefabs/Tile", typeof(GameObject)), new Vector3(x + xOff, y + yOff, 0), Quaternion.identity) as GameObject;
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
            tileGrid[xSelect, ySelect].DeSelectTile();
            xSelect--;
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }
    //move selection to the right if possible
    public void TileSelectionRight()
    {
        if (xSelect < boardSize - 1)
        {
            tileGrid[xSelect, ySelect].DeSelectTile();
            xSelect++;
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }
    //move selection to the up if possible
    public void TileSelectionUp()
    {
        if (ySelect < boardSize - 1)
        {
            tileGrid[xSelect, ySelect].DeSelectTile();
            ySelect++;
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }
    //move selection to the down if possible
    public void TileSelectionDown()
    {
        if (ySelect > 0)
        {
            tileGrid[xSelect, ySelect].DeSelectTile();
            ySelect--;
            tileGrid[xSelect, ySelect].SelectTile();
        }
    }

    //adds value to tile that is currently selected
    public void AddTileValue(int value)
    {
        tileGrid[xSelect, ySelect].SetPlayerValue(value);
    }
    
    //sets tiles to starting values
    //correct value is the value when the board is filled
    //current value is the value after removing numbers
    public void SetTileBoard(int x, int y, int currentValue, int correctValue)
    {
        tileGrid[x, y].SetStartingValue(currentValue, correctValue);
    }
}
