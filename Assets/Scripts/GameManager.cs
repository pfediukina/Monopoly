using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private UIManager UIManager;
    [SerializeField] private Board board;

    [Header("Players")]
    [SerializeField] private UnitController _currentPlayer;

    private void Start()
    {
        board.Init();
        _currentPlayer.Init();
    }

    //1 for left button and 0 for right button (if only one button shown, always 1)
    public void OnDialogResponse(DialogID dialog_id, bool responce)
    {
        UIManager.HidePlayerDialog();
    }

    public void OnPlayerRolled()
    {
        int num1 = Random.Range(1, 7);
        int num2 = Random.Range(1, 7);
        int sum = num1 + num2;
        _currentPlayer.MovePlayerWithStep(sum);
        DialogInfo dialog_info = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position].OnPlayerAtTile(_currentPlayer);
        UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Rent, dialog_info.Caption, dialog_info.Info, dialog_info.Button1, dialog_info.Button2);
    }
}
