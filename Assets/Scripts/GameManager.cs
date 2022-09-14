using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Board board;
    public List<UnitController> players;
    public CardInfo cards;

    private UIManager _UI;
    private UnitController _movingPlayer;
    private bool _onChance = false;
    private bool _onBankrupt = false;


    void Start()
    {
        board.Init();
        _UI = transform.GetComponent<UIManager>();

        for (int i = 0; i < players.Count; i++)
        {
            _movingPlayer = players[i];
            _movingPlayer.Init(board);
            _movingPlayer.SetCameraActive(false);
            _movingPlayer.Move(0);
        }
        _movingPlayer = players[0];

        _UI.UpdatePlayerMoney(_movingPlayer);
        _movingPlayer.SetCameraActive(true);


        //==============================
        _UI.GetSellPanel().SetDept(-140);

    }

    public void MoveCurrentPlayer(int step_)
    {
        var step = step_;
        ////=============
        //int ress;
        //int.TryParse(_UI.testStep.text, out ress);
        //if (ress != 0)
        //{
        //    step = ress;
        //    _UI.testStep.text = "0";
        //}
        ////=========================


        if (_movingPlayer.GetUnitInfo().jail <= 0)
            _movingPlayer.Move(step);
       
        _UI.HideInfoPanel();

        if (_movingPlayer.GetUnitInfo().position != 0 && (_movingPlayer.GetUnitInfo().position + step) / 10 >= 4)
        {
            _movingPlayer.GetUnitInfo().money += 200;
            _UI.UpdatePlayerMoney(_movingPlayer);
        }

        _UI.UpdatePlayerMoney(_movingPlayer);

        Tile t = board.GetTile(_movingPlayer.GetUnitInfo().position);
        TileInfo tileInfo = t.tileInfo;

        if (_movingPlayer.GetUnitInfo().jail > 0)
        {
            int num1 = Random.Range(1, 7), num2 = Random.Range(1, 7);
            if (num1 == num2)
            {
                _movingPlayer.GetUnitInfo().jail = 0;
                _UI.ShowInfoPanel("JAIL", $"You are in jail. \n" +
                $"You rolled {num1} and {num2}\n" +
                $"You get out of jail.", true);
            }
            else
            {
                _movingPlayer.GetUnitInfo().jail--;
                _UI.ShowInfoPanel("JAIL", $"You are in jail. \n" +
                $"You rolled {num1} and {num2}\n" +
                $"You don't get out of jail.", true);
            }
            return;
        }
        CheckColoredTiles(tileInfo, t);
        CheckSpecialTiles(tileInfo, t);
        CheckCornerTiles(tileInfo, t);
    }

    public void PlayerChoseButton(DialogResult result)
    {
        //Debug.Log(_movingPlayer.GetUnitInfo().position);
        Tile tile = board.GetTile(_movingPlayer.GetUnitInfo().position);
        //Debug.Log(_movingPlayer._unitInfo.money);
        if (result == DialogResult.YES)
        {
            if (_movingPlayer.GetUnitInfo().money - tile.tileInfo.Price < 0)
            {
                _UI.ShowInfoPanel("Information", "Not enought money", true);
            }
            else
            {
                _movingPlayer.GetUnitInfo().money -= tile.tileInfo.Price;
                _UI.SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, tile.tileInfo.Price, _movingPlayer.GetUnitInfo().color);
                _UI.ShowInfoPanel("Information", $"You bought {tile.tileInfo.Name} for {tile.tileInfo.Price}", true);
                tile.SetPlayer(_movingPlayer);
            }
        }
        else if (result == DialogResult.OKEY || result == DialogResult.NO)
        {
            if (_onChance && _movingPlayer.GetUnitInfo().jail == 0)
            {
                MoveCurrentPlayer(0);
                _onChance = false;
            }
            else if (_onBankrupt)
            {
                BankruptPlayer();
            }
            else
                ChangePlayer();

        }
        else if (result == DialogResult.SELL)
        {
            var sell = _UI.GetSellPanel();
            var tiles = sell.GetToggledElements();
            int sum = 0;

            foreach (var item in tiles)
            {
                sum += item.tileInfo.Price / 2;
                item.SetPlayer(null);
            }
            _movingPlayer.GetUnitInfo().money += sum;
            _UI.ShowInfoPanel("SELL", "You didnt lose!", true);
            sell.ShowPanel(false);
        }
        else if(result == DialogResult.LOSE)
        {
            _UI.GetSellPanel().ShowPanel(false);
            _onBankrupt = false;
            _UI.ShowInfoPanel("GAME OVER", "You lost!", true);
            _movingPlayer.Meshes[0].gameObject.SetActive(false);
        }
    }

    private void ChangePlayer()
    {
        if (players.Where(p => p.GetUnitInfo().money >= 0).Count() == 1)
        {
            _UI.ShowInfoPanel("WIN", $"{players[0].name} is winner!\nGame over", true);
            return;
        }
        _onChance = false;
        _onBankrupt = false;
        _movingPlayer.SetCameraActive(false);
        var id = (_movingPlayer.ID + 1) % 4;
        Debug.Log(_movingPlayer.ID + "/" + id);
        while (players[id].GetUnitInfo().money < 0)
        {
            id++;
            id = id % 4;
        }
        _movingPlayer = players[id];

        _UI.UpdatePlayerMoney(_movingPlayer);
        _movingPlayer.SetCameraActive(true);
        if (_movingPlayer.GetUnitInfo().jail > 0)
            MoveCurrentPlayer(0);
        _UI.UpdatePlayerMoney(_movingPlayer);

    }

    private void BankruptPlayer()
    {
        _onBankrupt = false;
        var playerTiles = board.GetTiles().Where(p => p.tileInfo.owningPlayer == _movingPlayer).ToList();
        int sum = 0;
        foreach (var item in playerTiles)
            sum += item.tileInfo.Price / 2;

        if(playerTiles.Count == 0 || _movingPlayer.GetUnitInfo().money + sum < 0)
        {
            _onBankrupt = false;
            _UI.ShowInfoPanel("GAME OVER", "You lost!", true);
            _movingPlayer.Meshes[0].gameObject.SetActive(false);
            foreach(var item in playerTiles)
            {
                item.SetPlayer(null);
            }
        }
        else
        {
            var sell = _UI.GetSellPanel();
            sell.ShowPanel(true);
            foreach(var tile in playerTiles)
            {
                sell.AddElementToList(tile.tileInfo.Name, tile.tileInfo.Price / 2, tile);
                _UI.rollButton.interactable = false;
                sell.SetDept(_movingPlayer.GetUnitInfo().money);
            }
        }
    }

    private void CheckColoredTiles(TileInfo tileInfo, Tile t)
    {
        RentInfo rentInfo = new RentInfo();
        if (tileInfo.tileType == TileType.Colored)
        {
            if (tileInfo.owningPlayer == null)
            {
                _UI.ShowBuyDialig(tileInfo); //Buy
            }
            else
            {
                if (_movingPlayer != tileInfo.owningPlayer)
                {

                    rentInfo = t.GetRent(board);
                    _UI.ShowRentDialig(tileInfo, rentInfo, _movingPlayer); // Rent

                    _movingPlayer.GetUnitInfo().money -= rentInfo.rent;
                    tileInfo.owningPlayer.GetUnitInfo().money += rentInfo.rent;

                    if (_movingPlayer.GetUnitInfo().money < 0)
                    {
                        _onBankrupt = true;
                    }
                }
                else
                {
                    _UI.ShowInfoPanel(tileInfo.Name, "You are owner", true);
                }
            }
        }
    }

    private void CheckSpecialTiles(TileInfo tileInfo, Tile t)
    {
        RentInfo rentInfo = new RentInfo();
        if (tileInfo.tileType == TileType.Special)
        {
            if (tileInfo.specialType == SpecialTile.Company)
            {
                if (tileInfo.owningPlayer == null)
                    _UI.ShowBuyDialig(tileInfo); // Buy
                else
                {
                    if (_movingPlayer != tileInfo.owningPlayer)
                    {
                        int num1 = Random.Range(1, 7), num2 = Random.Range(1, 7);
                        int rent = (num1 + num2) * 4;
                        var count = board.GetTiles().Where(p => (p.tileInfo.specialType == SpecialTile.Company && p.tileInfo.owningPlayer == tileInfo.owningPlayer && p.tileInfo.owningPlayer != null)).Count();


                        if (count == 2)
                            rent += (num1 + num2) * 6;

                        string desc = $"You rolled {num1} and {num2}\n" +
                                        $"You need to pay {(count == 1 ? 4 : 10)}*{num1 + num2}\n" +
                                        $"It's {rent}";
                        _UI.ShowInfoPanel(tileInfo.Name, desc, true); // Info
                        _movingPlayer.GetUnitInfo().money -= rent;
                        _UI.SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, rent, _movingPlayer.GetUnitInfo().color);
                        tileInfo.owningPlayer.GetUnitInfo().money += rentInfo.rent;
                        if (_movingPlayer.GetUnitInfo().money < 0)
                            _onBankrupt = true;
                    }
                }
            }
            else if (tileInfo.specialType == SpecialTile.Station)
            {
                if (tileInfo.owningPlayer == null)
                    _UI.ShowBuyDialig(tileInfo); // Buy
                else
                {
                    var count = board.GetTiles().Where(p => (p.tileInfo.specialType == SpecialTile.Station && p.tileInfo.owningPlayer == tileInfo.owningPlayer && p.tileInfo.owningPlayer != null)).Count();
                    int rent = 30 * count;
                    rentInfo.SetRent(rent, count);
                    _UI.ShowRentDialig(tileInfo, rentInfo, _movingPlayer); // Rent
                    _movingPlayer.GetUnitInfo().money -= rent;
                    tileInfo.owningPlayer.GetUnitInfo().money += rentInfo.rent;
                    if (_movingPlayer.GetUnitInfo().money < 0)
                        _onBankrupt = true;
                }
            }
            else if (tileInfo.specialType == SpecialTile.Tax)
            {
                _UI.ShowInfoPanel("Tax", $"You payed tax: {tileInfo.Price}", true); // Info
                _movingPlayer.GetUnitInfo().money -= tileInfo.Price;
                _UI.SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, tileInfo.Price, _movingPlayer.GetUnitInfo().color);
                if (_movingPlayer.GetUnitInfo().money < 0)
                    _onBankrupt = true;
            }
            else if (tileInfo.specialType == SpecialTile.Chance || tileInfo.specialType == SpecialTile.CommunityChest)
            {
                bool chance = tileInfo.specialType == SpecialTile.Chance ? true : false;
                int num = Random.Range(0, (chance ? cards.chanceInfo.Count: cards.chestInfo.Count));
                var card = chance ? cards.chanceInfo[num]: cards.chestInfo[num];

                if(card.Type == ChanceType.Posistion)
                { 
                        _onChance = true;
                        _movingPlayer.GetUnitInfo().position = card.Pos;
                }
                _movingPlayer.GetUnitInfo().money += card.Money;
                _UI.ShowInfoPanel(chance ? "CHANCE" : "CHEST", card.Desc, true);
                if(card.Money > 0)
                    _UI.SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, card.Money, _movingPlayer.GetUnitInfo().color, true);
                if (card.Money < 0)
                    _UI.SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, card.Money, _movingPlayer.GetUnitInfo().color);
            }
            else
            {
                string desc = $"Special Text";
                _UI.ShowInfoPanel(tileInfo.Name, desc, true); // Info
                _UI.UpdatePlayerMoney(_movingPlayer);
            }

        }
    }

    private void CheckCornerTiles(TileInfo tileInfo, Tile t)
    {
        if (tileInfo.tileType == TileType.Corner)
        {
            if (tileInfo.specialType == SpecialTile.Jail)
            {
                _movingPlayer.GetUnitInfo().jail = 3;
                _UI.ShowInfoPanel("JAIL", $"You are in jail. \n" +
                    $"Wait 3 turns and try to roll a double.", true);
            }
            else if(tileInfo.specialType == SpecialTile.Tax)
            {
                int tax = 40;
                var tiles = board.GetTiles();
                int count = tiles.Where(p => (p.tileInfo.owningPlayer == _movingPlayer && p.tileInfo.owningPlayer != null)).Count();
                _UI.ShowInfoPanel("TAX", $"You payed {count}*{tax}.", true);
                _movingPlayer.GetUnitInfo().money -= tax*count;
                _UI.SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, tax * count, _movingPlayer.GetUnitInfo().color);
            }
            else
            {
                string desc = $"You are at start";
                _UI.ShowInfoPanel(tileInfo.Name, desc, true);
                _UI.UpdatePlayerMoney(_movingPlayer);
            }
        }
    }

    
}
