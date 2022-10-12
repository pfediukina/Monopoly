using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CornerTaxTile : BaseTile
{    public override DialogInfo OnPlayerAtTile(UnitController unit, Board board)
    {
        DialogInfo d_info = new DialogInfo();
        //d_info.Title = tileInfo.Name;
        //int count = board.GetAllTiles().Where(p => (p.tileInfo.Owner == unit &&
        //                                            p.tileInfo.Owner != null)).Count();
        //tile_rent = count * tileInfo.Price;
        //if (count == 0)
        //{
        //    d_info.Desc = $"You don't have any property. You don't pay a tax.";
        //}
        //else
        //{
        //    d_info.Desc = $"You have paid a tax ({count} * {tileInfo.Price}). That's equal to {tile_rent}";
        //}
        //d_info.Button1 = "OK";
        //d_info.Button2 = "";
        //tile_rent *= -1;
        return d_info;
    }
}
