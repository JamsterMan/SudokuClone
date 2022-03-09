using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameBoard gameBoard;
    public DrawBoard drawBoard;

    private readonly int boardSize = 9;
    //private int[,] boardGrid = new int[9,9];

    // Start is called before the first frame update
    void Start()
    {
        int[,] boardGrid = gameBoard.GetGameBoardCopy();
        int[,] boardGridFilled = gameBoard.GetGameBoardCopy();

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                drawBoard.SetTileBoard(x, y, boardGrid[x, y], boardGridFilled[x,y]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveSelection();
        AddPlayerValue();
    }

    //moves the selected tile based on the arrows keys
    private void MoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))//move left
        {
            drawBoard.TileSelectionLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))//move right
        {
            drawBoard.TileSelectionRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))//move up
        {
            drawBoard.TileSelectionUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))//move down
        {
            drawBoard.TileSelectionDown();
        }
    }

    //adds inputed number to the grid
    private void AddPlayerValue()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) )//move left
        {
            drawBoard.AddTileValue(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))//move left
        {
            drawBoard.AddTileValue(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))//move left
        {
            drawBoard.AddTileValue(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))//move left
        {
            drawBoard.AddTileValue(4);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))//move left
        {
            drawBoard.AddTileValue(5);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))//move left
        {
            drawBoard.AddTileValue(6);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))//move left
        {
            drawBoard.AddTileValue(7);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))//move left
        {
            drawBoard.AddTileValue(8);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))//move left
        {
            drawBoard.AddTileValue(9);
        }
    }
}
