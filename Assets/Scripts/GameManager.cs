using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private UIManager UIManager;
    [SerializeField] private Board board;

    [Header("Players")]
    [SerializeField] private List<UnitController> _players;

    [Header("TEST")]
    [SerializeField] private TMPro.TMP_InputField textStep;

    private UnitController _currentPlayer;

    private void Start()
    {
        board.Init();
        foreach (UnitController player in _players)
        {
            player.Init();
            player.SetPlayerActive(false);
        }
        _currentPlayer = _players[0];
        _currentPlayer.SetPlayerActive(true);
    }

    //1 for left button and 0 for right button (if only one button shown, always 1)
    public void OnDialogResponse(DialogID dialog_id, bool responce)
    {
        UIManager.HidePlayerDialog();
        if (dialog_id == DialogID.Buy)
        {
            if (!responce)
            {
                ChangePlayer();
            }
            else
            {
                Tile tile = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
                tile.SetOwner(_currentPlayer);

                string caption = tile.GetTileInfo().Name;
                UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, caption, "You bounght it", "Okey", "");
            }
        }
        if (dialog_id == DialogID.Rent)
        {
            ChangePlayer();
        }
        if (dialog_id == DialogID.Info)
        {
            ChangePlayer();
        }
        if(dialog_id == DialogID.Jail)
        {
            if (_currentPlayer.GetPlayerInfo().Jail > 0)
            {
                ChangePlayer();
            }
        }
    }

    public void OnPlayerRolled()
    {
        int num1 = Random.Range(1, 7);
        int num2 = Random.Range(1, 7);
        int sum = num1 + num2;
        if(textStep.text != "0")
        {
            int.TryParse(textStep.text, out sum);
        }
        if(_currentPlayer.GetPlayerInfo().Jail == 0)
        {
            _currentPlayer.MovePlayerWithStep(sum);
        }
        DialogInfo dialog_info = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position].OnPlayerAtTile(_currentPlayer, board);
        UIManager.ShowPlayerDialog(_currentPlayer, dialog_info.ID, dialog_info.Caption, dialog_info.Info, dialog_info.Button1, dialog_info.Button2);

    }

    private void ChangePlayer()
    {
        if(_players.Count == 1)
        {
            // win
        }
        _currentPlayer.SetPlayerActive(false);
        int new_id = (_players.IndexOf(_currentPlayer) + 1) % _players.Count;
        _currentPlayer = _players[new_id];
        _currentPlayer.SetPlayerActive(true);
        if (_currentPlayer.GetPlayerInfo().Jail > 0)
            OnPlayerRolled();
    }
}
