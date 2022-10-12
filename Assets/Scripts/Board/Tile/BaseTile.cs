using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public TileInfo tileInfo;
    public int tile_rent;
    protected TileBuilder tileBuilder;

    public virtual void Init()
    {
        tileBuilder = GetComponent<TileBuilder>();
        tileBuilder.SetTileName(tileInfo.Name);
    }

    public abstract DialogInfo OnPlayerAtTile(UnitController unit, Board board);
    public virtual void SetOwner(UnitController unit) { }
}
