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
                tileGrid[x, y] = gameObject.GetComponent<TileControll>();
            }
        }
        tileGrid[0, 8].SelectTile();//0,8 so that selection starts at the top left corner
    }

    // Update is called once per frame
    void Update()
    {
        TileSelection();//move this to a game controller??

    }

    //changes Selected tile based on arrow key input
    private void TileSelection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))//move left
        {
            if (xSelect > 0)
            {
                tileGrid[xSelect, ySelect].DeSelectTile();
                xSelect--;
                tileGrid[xSelect, ySelect].SelectTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))//move right
        {
            if (xSelect < boardSize - 1)
            {
                tileGrid[xSelect, ySelect].DeSelectTile();
                xSelect++;
                tileGrid[xSelect, ySelect].SelectTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))//move up
        {
            if (ySelect < boardSize - 1)
            {
                tileGrid[xSelect, ySelect].DeSelectTile();
                ySelect++;
                tileGrid[xSelect, ySelect].SelectTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))//move down
        {
            if (ySelect > 0)
            {
                tileGrid[xSelect, ySelect].DeSelectTile();
                ySelect--;
                tileGrid[xSelect, ySelect].SelectTile();
            }
        }
    }

    public void SetTileBoard(int x, int y, int num)
    {
        tileGrid[x, y].SetStartingNumber(num);
    }
}
