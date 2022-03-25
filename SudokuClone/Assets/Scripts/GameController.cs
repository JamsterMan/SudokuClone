﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameBoard gameBoard;
    public DrawBoard drawBoard;
    public bool checkCorrectness = false;
    public bool addNotes = false;
    public int difficultyIncrease = 5;
    public GameObject pauseMenu;

    private readonly int boardSize = 9;
    private bool isGameDone = false;
    private bool checkCorrectnessChange;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        gameBoard.removeAttempts = difficultyIncrease;
        SetUpGameBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            MoveSelection();
            if (isGameDone == false)
            {
                PlayerInput();
                if (Input.GetKeyDown(KeyCode.F))//for testing, fills board with correct values
                {
                    CompleteBoard();
                }

                if (checkCorrectness != checkCorrectnessChange)
                {
                    checkCorrectnessChange = checkCorrectness;
                    drawBoard.SetBoardCorrectness(checkCorrectness);
                }

                if (drawBoard.IsBoardCorrect())
                {
                    Debug.Log("Game Win!");
                    isGameDone = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!isPaused);
            isPaused = !isPaused;
        }
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
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(1);
            else
                drawBoard.AddTileValue(1, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(2);
            else
                drawBoard.AddTileValue(2, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(3);
            else
                drawBoard.AddTileValue(3, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(4);
            else
                drawBoard.AddTileValue(4, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(5);
            else
                drawBoard.AddTileValue(5, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(6);
            else
                drawBoard.AddTileValue(6, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(7);
            else
                drawBoard.AddTileValue(7, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(8);
            else
                drawBoard.AddTileValue(8, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))//move left
        {
            if (addNotes)
                drawBoard.AddNoteValue(9);
            else
                drawBoard.AddTileValue(9, checkCorrectness);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            drawBoard.RemoveTileValue();
        }
    }

    //for testing to quickly solve a game board
    private void CompleteBoard()
    {
        drawBoard.CompleteBoardTiles();
    }

    //fills tile with the correct starting values for a generated game board
    private void SetUpGameBoard()
    {
        checkCorrectnessChange = checkCorrectness;
        int[,] boardGrid = gameBoard.GetGameBoardCopy();
        int[,] boardGridFilled = gameBoard.GetGameBoard();

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                drawBoard.SetTileBoard(x, y, boardGrid[x, y], boardGridFilled[x, y]);
            }
        }
    }

    //get a new board layout
    public void NewBoard()
    {
        isGameDone = false;
        gameBoard.NewGameBoard();
        drawBoard.ResetTiles();
        SetUpGameBoard();
        Debug.Log("New Game Started");
    }

    //starts/stops checking for player errors
    public void CheckForMistakes(bool checkMistakes)
    {
        checkCorrectness = checkMistakes;
    }

    public void SetAddNotes(bool notes)
    {
        addNotes = notes;
    }

    //undoes the last player added/removed value
    public void Undo()
    {
        drawBoard.UndoTiles(checkCorrectness);
    }

    //Sets isPaused = false to allow the game to be played again
    public void ResumeGame()
    {
        isPaused = false;
    }

    //changes difficulty of the board and generates a new one
    //easy = 0, medium = 1, hard = 2, expert = 3
    public void ChangeDifficulty(int val)
    {
        gameBoard.removeAttempts = (val*difficultyIncrease) + difficultyIncrease;
        NewBoard();
    }
}
