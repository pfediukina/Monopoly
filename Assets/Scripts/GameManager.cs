using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        DialogInfo info = new DialogInfo();
        info.Title = "Hello World";
        info.Desc = "Hello World";
        info.Button1 = "Hello";
        info.Button2 = "World";
        MessageDialog dialog = new MessageDialog();
        dialog.Init();
        dialog.SetDialogInfo(info);
        dialog.ShowPlayerDialog();
    }
}

//    //1 for left button and 0 for right button (if only one button shown, always 1)
//    public void OnDialogResponse(DialogID dialog_id, bool responce)
//    {
//        UIManager.HideDialogs();

//        if (dialog_id == DialogID.Buy)
//        {
//            if (!responce)
//            {
//                ChangePlayer();
//            }
//            else
//            {
//                BaseTile tile = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
//                int start_money = _currentPlayer.GetPlayerInfo().Money;
//                bool enough_money = _currentPlayer.AddPlayerMoney(tile.tile_rent);
//                string caption = tile.tileInfo.Name;
//                if (!enough_money)
//                    UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, caption, "Not enoungh money!", "OK", "");
//                else
//                {
//                    UIManager.UpdatePlayerMoney(start_money, tile.tile_rent, _currentPlayer.GetPlayerInfo().Money);
//                    tile.SetOwner(_currentPlayer);
//                    UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, caption, "You bounght it", "OK", "");
//                    UIManager.UpdatePropertyList(_currentPlayer, board);
//                }
//            }
//        }

//        if (dialog_id == DialogID.Rent)
//        {
//            BaseTile tile = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
//            if(tile.tileInfo.Owner != null)
//                tile.tileInfo.Owner.AddPlayerMoney(Mathf.Abs(tile.tile_rent));
//            if(_currentPlayer.GetPlayerInfo().Money > 0)
//                ChangePlayer();
//        }

//        if (dialog_id == DialogID.Info)
//        {
//            ChangePlayer();
//        }

//        if(dialog_id == DialogID.Jail)
//        {
//            if (_currentPlayer.GetPlayerInfo().Jail > 0)
//            {
//                ChangePlayer();
//            }
//        }

//        if(dialog_id == DialogID.Chance)
//        {
//            DialogInfo dialog_info = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position].OnPlayerAtTile(_currentPlayer, board);
//            UIManager.ShowPlayerDialog(_currentPlayer, dialog_info.ID, dialog_info.Caption, dialog_info.Info, dialog_info.Button1, dialog_info.Button2);
//        }

//        if (dialog_id == DialogID.Chest)
//        {
//            BaseTile tile = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
//            int start_money = _currentPlayer.GetPlayerInfo().Money;
//            bool enough_money = _currentPlayer.AddPlayerMoney(tile.tile_rent);

//            //lose
//            UIManager.UpdatePlayerMoney(start_money, tile.tile_rent, _currentPlayer.GetPlayerInfo().Money);
//            ChangePlayer();
//        }

//        if(dialog_id == DialogID.Bankrupt)
//        {
//            var lost_player = _currentPlayer;
//            ChangePlayer();
//            lost_player.HidePlayer();
//        }
//        if(dialog_id == DialogID.List)
//        {
//            if(responce)
//            {
//                List<Tile> list = UIManager.GetList().GetCheckedList();
//                int money = 0;
//                foreach(Tile tile in list)
//                {
//                    money += tile.GetTileInfo().Price / 2;
//                    tile.SetOwner(null);
//                    if (_currentPlayer.GetPlayerInfo().Money > 0)
//                        UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Sell, "PROPERTY", "You selled property", "OK", "");
//                    else
//                        UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Info, "PROPERTY", "You selled property", "OK", "");
//                }
//                UIManager.UpdatePlayerMoney(_currentPlayer.GetPlayerInfo().Money, money, _currentPlayer.GetPlayerInfo().Money + money);
//                _currentPlayer.GetPlayerInfo().Money += money;
//            }
//            else
//            {
//                if (_currentPlayer.GetPlayerInfo().Money <= 0)
//                {
//                    UIManager.HideDialogs();
//                    UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Bankrupt, "BANKRUPT", "You lost!", "OK :c", "");
//                }
//            }
//        }

//        if(dialog_id == DialogID.Winner)
//        {
//            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
//            return;
//        }

//        if(dialog_id != DialogID.List)
//            CheckPlayerMoney();
//    }

//    public void CheckPlayerMoney()
//    {
//        if(_currentPlayer.GetPlayerInfo().Money <= 0)
//        {
//            var player_property = board.GetAllTiles().Where(p => p.tileInfo.Owner == _currentPlayer &&
//                                                                 p.tileInfo.Owner != null).ToList();
//            int property_price = 0;
//            foreach(var p in player_property)
//            {
//                property_price += p.tileInfo.Price / 2;
//            }
//            if (property_price <= Mathf.Abs(_currentPlayer.GetPlayerInfo().Money))
//            {
//                UIManager.HideDialogs();
//                UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Bankrupt, "BANKRUPT", "You dont have money!", "OK :c", "");
//                foreach (var p in player_property)
//                {
//                    p.SetOwner(null);
//                }
//            }
//            else
//            {
//                UIManager.OnPlayerList();
//            }
//        }
//    }

//    public void OnPlayerRolled()
//    {
//        int num1 = Random.Range(1, 7);
//        int num2 = Random.Range(1, 7);
//        int sum = num1 + num2;

//        if(textStep.text != "0")
//        {
//            int.TryParse(textStep.text, out sum);
//        }

//        if(_currentPlayer.GetPlayerInfo().Jail == 0)
//        {
//            if (_currentPlayer.GetPlayerInfo().Position + sum >= 40)
//                _currentPlayer.AddPlayerMoney(200);
//            _currentPlayer.MovePlayerWithStep(sum);
//        }
//        DialogInfo dialog_info = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position].OnPlayerAtTile(_currentPlayer, board);
//        UIManager.ShowPlayerDialog(_currentPlayer, dialog_info.ID, dialog_info.Caption, dialog_info.Info, dialog_info.Button1, dialog_info.Button2);


//        if (dialog_info.ID == DialogID.Rent || dialog_info.ID == DialogID.Chest)
//        {
//            var t = board.GetAllTiles()[_currentPlayer.GetPlayerInfo().Position];
//            int start_money = _currentPlayer.GetPlayerInfo().Money;
//            bool enough_money = _currentPlayer.AddPlayerMoney(t.tile_rent);
//            UIManager.UpdatePlayerMoney(start_money, t.tile_rent, _currentPlayer.GetPlayerInfo().Money);
//        }

//    }

//    public UnitController GetCurrentPlayer()
//    {
//        return _currentPlayer;
//    }
//    public List<BaseTile> GetPlayerProperty(UnitController player)
//    {
//        List<BaseTile> tiles = board.GetAllTiles().Where(p => p.tileInfo.Owner != null && p.tileInfo.Owner == player).ToList();
//        return tiles;
//    }

//    private void ChangePlayer()
//    {
//        _currentPlayer.SetPlayerActive(false);
//        int new_id = (_players.IndexOf(_currentPlayer) + 1) % _players.Count;

//        while (_players[new_id].GetPlayerInfo().Money < 0)
//        {
//            new_id++;
//            new_id = new_id % _players.Count;
//        }
//        _currentPlayer = _players[new_id];
//        _currentPlayer.SetPlayerActive(true);

//        UIManager.UpdatePropertyList(_currentPlayer, board);
//        UIManager.UpdatePlayerMoney(_currentPlayer.GetPlayerInfo().Money, _currentPlayer.GetPlayerInfo().Color);

//        if (_players.Where(p => p.GetPlayerInfo().Money > 0).Count() == 1)
//        {
//            UIManager.ShowPlayerDialog(_currentPlayer, DialogID.Winner, "WINNER", "Winner Winner Chicken Dinner!", ":D", "");
//            return;
//        }

//        if (_currentPlayer.GetPlayerInfo().Jail > 0)
//            OnPlayerRolled();
//    }
//}
