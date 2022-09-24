using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TileInfo tileInfo;
    [SerializeField] private TileBuilder tileBuilder;

    [Header("Test Mode")]
    [SerializeField] private bool update;

    private int tile_rent = 0; // - or +

    private void Update()
    {
        if(update)
        {
            Init();
            update = false;
        }
    }

    public TileInfo GetTileInfo()
    {
        return tileInfo;
    }
    public void SetTileInfo(TileInfo info)
    {
        if (info == null) return;
        tileInfo = info;
    }

    public void Init()
    {
        bool is_colored = tileInfo.Type == TileType.Colored ? true : false;

        tileBuilder.SetTileName(tileInfo.Name, is_colored);

        if(tileInfo.Price != 0)
            tileBuilder.SetTilePrice(tileInfo.Price);

        tileBuilder.SetTileColor(tileInfo.Color, is_colored);
    }

    public int GetPlayerRent()
    {
        return tile_rent;
    }

    public void SetOwner(UnitController player)
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

    public DialogInfo OnPlayerAtTile(UnitController player, Board board)
    {
        DialogInfo d_info;
        AtTile atTileEvent = new AtTile();
        atTileEvent.SetupScript(this, player, board);
        d_info = atTileEvent.OnPlayerAtTile();
        return d_info;
    }
}
