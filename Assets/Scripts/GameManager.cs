using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public sBoard board;
    public UnitController player1;

    void Start()
    {
        if (board != null && player1 != null)
        {
            board.InitBoard();
            player1.InitPlayer(board);
        }
    }
}
