using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        UIManager.UpdatePlayerMoney(_currentPlayer.GetPlayerInfo().Money, _currentPlayer.GetPlayerInfo().Color);
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
                int start_money = _currentPlayer.GetPlayerInfo().Money;
                bool enough_money = _currentPlayer.AddPlayerMoney(tile.GetPlayerRent());
                string caption = tile.GetTileInfo().Name;
                if (!enough_money)
                    UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, caption, "Not enoungh money!", "OK", "");
                else
                {
                    UIManager.UpdatePlayerMoney(start_money, tile.GetPlayerRent(), _currentPlayer.GetPlayerInfo().Money);
                    tile.SetOwner(_currentPlayer);
                    UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, caption, "You bounght it", "OK", "");
                    UIManager.UpdatePropertyList(_currentPlayer, board);
                }
            }
        }

        if (dialog_id == DialogID.Rent)
        {
            Tile tile = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
            if(tile.GetTileInfo().Owner != null)
                tile.GetTileInfo().Owner.AddPlayerMoney(Mathf.Abs(tile.GetPlayerRent()));
            CheckPlayerMoney();
            //ChangePlayer();
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

        if(dialog_id == DialogID.Chance)
        {
            DialogInfo dialog_info = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position].OnPlayerAtTile(_currentPlayer, board);
            UIManager.ShowPlayerDialog(_currentPlayer, dialog_info.ID, dialog_info.Caption, dialog_info.Info, dialog_info.Button1, dialog_info.Button2);
        }

        if (dialog_id == DialogID.Chest)
        {
            Tile tile = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
            int start_money = _currentPlayer.GetPlayerInfo().Money;
            bool enough_money = _currentPlayer.AddPlayerMoney(tile.GetPlayerRent());

            //lose
            UIManager.UpdatePlayerMoney(start_money, tile.GetPlayerRent(), _currentPlayer.GetPlayerInfo().Money);
            ChangePlayer();
        }

        if(dialog_id == DialogID.Bankrupt)
        {
            var lost_player = _currentPlayer;
            ChangePlayer();
            lost_player.HidePlayer();
        }
    }


    public void CheckPlayerMoney()
    {
        if(_currentPlayer.GetPlayerInfo().Money <= 0)
        {
            var player_property = board.GetAllTiles().Where(p => p.GetTileInfo().Owner == _currentPlayer &&
                                                                 p.GetTileInfo().Owner != null).ToList();
            int property_price = 0;
            foreach(var p in player_property)
            {
                property_price += p.GetTileInfo().Price / 2;
            }
            if (property_price <= Mathf.Abs(_currentPlayer.GetPlayerInfo().Money))
            {
                UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Bankrupt, "BANKRUPT", "You dont have money!", "OK :c", "");
                foreach (var p in player_property)
                {
                    p.SetOwner(null);
                }
            }
        }
        else
        {
            ChangePlayer();
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
        
        if (dialog_info.ID == DialogID.Rent || dialog_info.ID == DialogID.Chest)
        {
            var t = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
            int start_money = _currentPlayer.GetPlayerInfo().Money;
            bool enough_money = _currentPlayer.AddPlayerMoney(t.GetPlayerRent());
            UIManager.UpdatePlayerMoney(start_money, t.GetPlayerRent(), _currentPlayer.GetPlayerInfo().Money);
        }

    }

    private void ChangePlayer()
    {
        _currentPlayer.SetPlayerActive(false);
        int new_id = (_players.IndexOf(_currentPlayer) + 1) % _players.Count;

        while (_players[new_id].GetPlayerInfo().Money < 0)
        {
            new_id++;
            new_id = new_id % _players.Count;
        }
        _currentPlayer = _players[new_id];
        _currentPlayer.SetPlayerActive(true);

        UIManager.UpdatePropertyList(_currentPlayer, board);
        UIManager.UpdatePlayerMoney(_currentPlayer.GetPlayerInfo().Money, _currentPlayer.GetPlayerInfo().Color);

        if (_players.Where(p => p.GetPlayerInfo().Money > 0).Count() == 1)
        {
            UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, "WINNER", "Winner Winner Chicken Dinner!", ":D", "");
            return;
        }

        if (_currentPlayer.GetPlayerInfo().Jail > 0)
            OnPlayerRolled();
    }
}
