using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompanyTile : BaseTile
{
    public override void Init()
    {
        base.Init();
        tileBuilder.SetTilePrice(tileInfo.Price);
        tileBuilder.SetNameOffset();
    }

    public override DialogInfo OnPlayerAtTile(UnitController unit, Board board)
    {
        DialogInfo d_info = new DialogInfo();
        //int num1 = Random.Range(1, 7);
        //int num2 = Random.Range(1, 7);

        //int count = board.GetAllTiles().Where(p => (p.tileInfo.Type == TileType.Company &&
        //                                            p.tileInfo.Owner == tileInfo.Owner &&
        //                                            p.tileInfo.Owner != null)).Count();
        //tile_rent = count == 2 ? (num1 + num2) * 6 : (num1 + num2) * 4;
        //tile_rent *= 3;

        //d_info.Caption = tileInfo.Name;
        //d_info.Info = $"You rolled {num1} and {num2}. " +
        //                $"You need to pay {num1 + num2} * {(count == 2 ? 6 : 4)} * 3. " +
        //                $"It's {tile_rent}";
        //d_info.ID = DialogID.Rent;
        //d_info.Button1 = "OK";
        //d_info.Button2 = "";
        //tile_rent *= -1;
        return d_info;
    }
    public override void SetOwner(UnitController player)
    {
        if (player != null)
        {
            tileInfo.Owner = player;
            tileBuilder.SetTileOwnerColor(player.GetPlayerInfo().Color);
        }
        else
        {
            tileInfo.Owner = null;
            tileBuilder.HideOwnerColorMesh();
        }

    }
}
