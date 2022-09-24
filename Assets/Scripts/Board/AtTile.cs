using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AtTile
{
    public int tile_rent = 0;

    private Tile _tile;
    private UnitController _player;
    private Board _board;

    public void SetupScript(Tile tile, UnitController player, Board board)
    {
        _player = player;
        _tile = tile;
        _board = board;
    }

    public DialogInfo OnPlayerAtTile()
    {
        DialogInfo d_info;
        if (_tile.GetTileInfo().Owner == null && (_tile.GetTileInfo().Type == TileType.Colored ||
            _tile.GetTileInfo().Type == TileType.Station || _tile.GetTileInfo().Type == TileType.Company))
        {
            d_info = ReturnBuyDialog();
            return d_info;
        }

        if (_tile.GetTileInfo().Owner == _player)
        {
            d_info = new DialogInfo();
            d_info.Caption = _tile.GetTileInfo().Name;
            d_info.Info = $"It is your property";
            d_info.Button1 = "OK";
            d_info.Button2 = "";
            return d_info;
        }

        switch (_tile.GetTileInfo().Type)
        {
            case TileType.Colored:
                d_info = OnPlayerAtColoredOrStationTile();
                break;
            case TileType.Station:
                d_info = OnPlayerAtColoredOrStationTile();
                break;
            case TileType.Company:
                d_info = OnPlayerAtCompanyTile();
                break;
            //case TileType.Chance:
            //    break;
            //case TileType.Chest:
            //    break;
            case TileType.Start:
                d_info = OnPlayerAtStart();
                break;
            case TileType.CornerTax:
                d_info = OnPlayerAtTax();
                break;
            case TileType.Tax:
                d_info = OnPlayerAtTax();
                break;
            case TileType.Jail:
                d_info = OnPlayerAtJail();
                break;
            default:
                d_info = new DialogInfo();
                break;
        }
        return d_info;
    }

    private DialogInfo OnPlayerAtColoredOrStationTile()
    {
        DialogInfo d_info = new DialogInfo();
        int counts = 0;
        if (_tile.GetTileInfo().Type == TileType.Colored)
        {
            counts = _board.GetAllTiles().Where(p => p.GetTileInfo().Type == TileType.Colored &&
                                                    p.GetTileInfo().Owner == _tile.GetTileInfo().Owner &&
                                                    p.GetTileInfo().Owner != null &&
                                                    p.GetTileInfo().Color == _tile.GetTileInfo().Color).Count();
        }
        else
        {
            counts = _board.GetAllTiles().Where(p => p.GetTileInfo().Type == TileType.Station &&
                                                    p.GetTileInfo().Owner == _tile.GetTileInfo().Owner &&
                                                    p.GetTileInfo().Owner != null).Count();
        }
        tile_rent = counts * _tile.GetTileInfo().Price / 2;
        d_info.Caption = _tile.GetTileInfo().Name;
        d_info.Info = $"You have entered {_tile.GetTileInfo().Owner.name} property. " +
            $"You will pay {counts} * {_tile.GetTileInfo().Price / 2}. This is equal to {tile_rent}";
        d_info.ID = DialogID.Rent;
        d_info.Button1 = "OK";
        d_info.Button2 = "";
        return d_info;
    }

    private DialogInfo OnPlayerAtCompanyTile()
    {
        DialogInfo d_info = new DialogInfo();
        int num1 = Random.Range(1, 7);
        int num2 = Random.Range(1, 7);

        int count = _board.GetAllTiles().Where(p => (p.GetTileInfo().Type == TileType.Company &&
                                                    p.GetTileInfo().Owner == _tile.GetTileInfo().Owner &&
                                                    p.GetTileInfo().Owner != null)).Count();
        tile_rent = count == 2 ? (num1 + num2) * 6 : (num1 + num2) * 4;
        tile_rent *= 3;

        d_info.Caption = _tile.GetTileInfo().Name;
        d_info.Info = $"You rolled {num1} and {num2}. " +
                        $"You need to pay {num1 + num2} * {(count == 2 ? 6 : 4)} * 3. " +
                        $"It's {tile_rent}";
        d_info.ID = DialogID.Rent;
        d_info.Button1 = "OK";
        d_info.Button2 = "";

        return d_info;
    }

    private DialogInfo OnPlayerAtTax()
    {
        DialogInfo d_info = new DialogInfo();
        d_info.Caption = _tile.GetTileInfo().Name;

        if (_tile.GetTileInfo().Type == TileType.Tax)
        {
            d_info.Info = $"You have paid a tax ({_tile.GetTileInfo().Price}).";
            tile_rent = _tile.GetTileInfo().Price;
        }
        else
        {
            int count = _board.GetAllTiles().Where(p => (p.GetTileInfo().Owner == _player &&
                                                        p.GetTileInfo().Owner != null)).Count();
            tile_rent = count * _tile.GetTileInfo().Price;
            if (count == 0)
                d_info.Info = $"You don't have any property. You don't pay a tax.";
            else
                d_info.Info = $"You have paid a tax ({count} * {_tile.GetTileInfo().Price}). That's equal to {tile_rent}";
        }

        d_info.ID = DialogID.Rent;
        d_info.Button1 = "OK";
        d_info.Button2 = "";
        return d_info;
    }

    private DialogInfo OnPlayerAtStart()
    {
        DialogInfo d_info = new DialogInfo();
        d_info.ID = DialogID.Info;
        d_info.Caption = _tile.GetTileInfo().Name;
        d_info.Info = $"You're at the start. All over again.";
        d_info.Button1 = "OK";
        d_info.Button2 = "";
        return d_info;
    }

    private DialogInfo OnPlayerAtJail()
    {
        DialogInfo d_info = new DialogInfo();
        d_info.ID = DialogID.Jail;
        d_info.Caption = _tile.GetTileInfo().Name;
        if (_player.GetPlayerInfo().Jail == 0)
        {
            d_info.Info = $"You are in jail. Wait 3 turns and try to roll a double.";
            _player.GetPlayerInfo().Jail = 3;
            d_info.Button1 = "OK";
            d_info.Button2 = "";
            return d_info;
        }
        int num1 = Random.Range(1, 7);
        int num2 = Random.Range(1, 7);
        _player.GetPlayerInfo().Jail--;
        if (_player.GetPlayerInfo().Jail <= 0 && num1 != num2) // if 3rd roll
        {
            d_info.Info = $"You waited 3 moves. You're out of jail";
        }
        else if (num1 == num2) // if double
        {
            d_info.Info = $"You rolled {num1} and {num2}. You're out of jail";
            _player.GetPlayerInfo().Jail = 0;
        }
        else // if not 3rd and not double
        {
            d_info.Info = $"You rolled {num1} and {num2}. You're not out of jail";
        }
        
        d_info.Button1 = "OK";
        d_info.Button2 = "";
        return d_info;
    }

    private DialogInfo ReturnBuyDialog()
    {
        DialogInfo d_info = new DialogInfo();
        d_info.ID = DialogID.Buy;
        d_info.Caption = _tile.GetTileInfo().Name;
        d_info.Info = $"Do you want buy {_tile.GetTileInfo().Name} for {_tile.GetTileInfo().Price}?";
        d_info.Button1 = "Yes";
        d_info.Button2 = "No";
        return d_info;
    }
}
