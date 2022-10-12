using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : BaseTile
{
    public override DialogInfo OnPlayerAtTile(UnitController unit, Board board)
    {
        DialogInfo d_info = new DialogInfo();
        d_info.Title = tileInfo.Name;

        d_info.Desc = "You are at start tile.";

        d_info.Button1 = "OK";
        d_info.Button2 = "";
        return d_info;
    }
}
