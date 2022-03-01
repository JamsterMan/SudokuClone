using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    private int[,] gameBoard = new int[9,9];
    private readonly int size = 9;

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
        FillBoard();

        //display board
        string game = "\n";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                game += gameBoard[i, j] + " ";
            }
            game += "\n";
        }
        Debug.Log(game);
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
    private void FillBoard()
    {
        while (!CheckBoard())
        {
            //int row = Random.Range(1, 9);
            //int col = Random.Range(1, 9);
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (gameBoard[row, col] == 0)
                    {
                        int value = Random.Range(1, 9);
                        if (CheckCorrectPlacement(row, col, value))
                        {
                            gameBoard[row, col] = value;
                        }
                    }
                }
            }
        }
    }
    //checks if value is already in the row, col, or 3x3 square area
    private bool CheckCorrectPlacement(int row, int col, int value)
    {
        for(int j = 0; j < size; j++)//check the row for value
        {
            if (gameBoard[row, j] == value && j != col)
                return false;
        }
        for (int i = 0; i < size; i++)// check the column for value
        {
            if (gameBoard[i, col] == value && i != row)
                return false;
        }
        for(int i = 3*(row%3); i < 3*(row%3) + 3; i++)//rows of the 3x3 square that row, col exists in
        {
            for (int j = 3 * (col % 3); j < 3 * (col % 3) + 3; j++)//Cols of the 3x3 square that row, col exists in
            {
                if (gameBoard[i, j] == value && (i != row || j != col))
                    return false;
            }
        }
        return true;
    }

    //checks if there are any empty spaces left on the game board
    private bool CheckBoard()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (gameBoard[i, j] == 0)
                    return false;
            }
        }
        return true;
    }


}
