using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxTile : BaseTile
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
        //d_info.Caption = tileInfo.Name;

        //d_info.Info = $"You have paid a tax ({tileInfo.Price}).";

        //d_info.ID = DialogID.Rent;
        //tile_rent = tileInfo.Price;
        
        //d_info.Button1 = "OK";
        //d_info.Button2 = "";
        //tile_rent *= -1;
        return d_info;
    }
}
