using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTile : BaseTile
{
    public override void Init()
    {
        base.Init();
        tileBuilder.SetTilePrice(tileInfo.Price);
        tileBuilder.SetNameOffset();
    }

    
}
