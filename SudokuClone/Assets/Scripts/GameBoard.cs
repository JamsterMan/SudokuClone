using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public int removeAttempts = 5;

    private int[,] gameBoard = new int[9,9];
    private readonly int size = 9;

    private int[,] gameBoardCopy = new int[9, 9];
    private int solutionCount;
    private bool tooManySolutions;

    // Start is called before the first frame update
    void Awake()
    {
        NewGameBoard();
    }

    //set all values to 0 to help board creation
    private void InitBoard()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                gameBoard[row, col] = 0;
            }
        }
    }

    //fill the board with numbers to make a correct sudoku game board
    private bool FillBoard()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (gameBoard[row, col] == 0)
                {
                    int i = Random.Range(1, 10);//used to randomized the board
                    for (int value = i; value < i+size; value++) {
                        if (CheckCorrectPlacement(row, col, (value % 9) + 1, gameBoard))
                        {
                            gameBoard[row, col] = (value%9)+1;

                            if (CheckBoard(gameBoard))
                                return true;
                            else
                                if (FillBoard())
                                    return true;
                        }
                    }
                    gameBoard[row, col] = 0;
                    return false;
                }
            }
        }
        return false;
    }
    //checks if value is already in the row, col, or 3x3 square area
    private bool CheckCorrectPlacement(int row, int col, int value, int[,] board)
    {
        for(int j = 0; j < size; j++)//check the row for value
        {
            if (board[row, j] == value && j != col)
                return false;
        }
        for (int i = 0; i < size; i++)// check the column for value
        {
            if (board[i, col] == value && i != row)
                return false;
        }
        for(int i = 3*(row / 3); i < 3*(row / 3) + 3; i++)//rows of the 3x3 square that row, col exists in
        {
            for (int j = 3 * (col / 3); j < 3 * (col / 3) + 3; j++)//Cols of the 3x3 square that row, col exists in
            {
                if (board[i, j] == value && (i != row || j != col))
                    return false;
            }
        }
        return true;
    }

    //checks if there are any empty spaces left on the game board
    private bool CheckBoard(int[,] board)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == 0)
                    return false;
            }
        }
        return true;
    }

    //Sets a random tile to 0, and check if it has only one solution
    private void RemoveBoardValues()
    {
        int attempts = removeAttempts;
        while(attempts > 0)
        {
            int row = Random.Range(0, 9);
            int col = Random.Range(0, 9);
            while(gameBoardCopy[row, col] == 0)
            {
                row = Random.Range(0, 9);
                col = Random.Range(0, 9);
            }

            int tempValue = gameBoardCopy[row, col];
            gameBoardCopy[row, col] = 0;

            solutionCount = 0;
            tooManySolutions = false;
            SolveBoard();
            if (solutionCount != 1)
            {
                gameBoardCopy[row, col] = tempValue;
                attempts--;
            }
        }
    }

    //trys to solve the current board + counts how many solutions there are (we only want one posible solution)
    private void SolveBoard()
    {
        if (tooManySolutions)
            return;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (gameBoardCopy[row, col] == 0)
                {
                    for (int value = 1; value < size+1; value++)//loop throgh possible values
                    {
                        if (CheckCorrectPlacement(row, col, value, gameBoardCopy))
                        {
                            gameBoardCopy[row, col] = value;

                            if (CheckBoard(gameBoardCopy))
                            {
                                solutionCount++;//track number of solutions
                                if(solutionCount > 1)
                                {
                                    tooManySolutions = true;
                                    return;
                                }
                                gameBoardCopy[row, col] = 0;//dont waste time trying 2-9 if one is the solution for the final tile
                                return;
                            }
                            else
                                SolveBoard();
                        }
                    }
                    gameBoardCopy[row, col] = 0;
                    return;//prevent double counting (i.e add val to 1,1 then 2,2 and 2,2 then 1,1 (row,col))
                }
            }
        }
    }

    //makes a copy of the game board
    private void CopyBoard()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                gameBoardCopy[row,col] = gameBoard[row, col];
            }
        }
    }

    //retuns the game board copy
    public int[,] GetGameBoardCopy()
    {
        return gameBoardCopy;
    }

    //returns the game board
    public int[,] GetGameBoard()
    {
        return gameBoard;
    }

    //resets the game board
    public void NewGameBoard()
    {
        InitBoard();
        FillBoard();
        CopyBoard();
        RemoveBoardValues();
    }
}
