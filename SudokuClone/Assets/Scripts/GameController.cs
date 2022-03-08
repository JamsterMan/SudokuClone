using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameBoard gameBoard;
    public DrawBoard drawBoard;

    private readonly int boardSize = 9;
    private int[,] boardGrid = new int[9,9];

    // Start is called before the first frame update
    void Start()
    {
        boardGrid = gameBoard.GetGameBoardCopy();
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                drawBoard.SetTileBoard(x, y, boardGrid[x, y]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
