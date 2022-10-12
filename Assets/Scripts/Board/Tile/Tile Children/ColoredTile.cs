using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColoredTile : BaseTile
{
    public override DialogInfo OnPlayerAtTile(UnitController unit, Board board)
    {
        DialogInfo d_info = new DialogInfo();
        //int counts = 0;
        //    counts = board.GetAllTiles().Where(p => p.tileInfo.Type == TileType.Colored &&
        //                                            p.tileInfo.Owner == tileInfo.Owner &&
        //                                            p.tileInfo.Owner != null &&
        //                                            p.tileInfo.Color == tileInfo.Color).Count();
        //tile_rent = counts * tileInfo.Price / 2;
        //d_info.Caption = tileInfo.Name;
        //d_info.Info = $"You have entered {tileInfo.Owner.name} property. " +
        //    $"You will pay {counts} * {tileInfo.Price / 2}. This is equal to {tile_rent}";
        //d_info.ID = DialogID.Rent;
        //d_info.Button1 = "OK";
        //d_info.Button2 = "";
        //tile_rent *= -1;
        return d_info;
    }

    public override void Init()
    {
        base.Init();
        tileBuilder.SetTileColor(tileInfo.Color);
        tileBuilder.SetTilePrice(tileInfo.Price);
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
