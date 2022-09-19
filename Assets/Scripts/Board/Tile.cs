using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileInfo tileInfo;
    [SerializeField] private TileBuilder tileBuilder;

    [Header("Test Mode")]
    [SerializeField] private bool update;

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

    public DialogInfo OnPlayerAtTile(UnitController player)
    {
        DialogInfo d_info = new DialogInfo();
        if(tileInfo.Type == TileType.Colored)
        {
            d_info.Caption = tileInfo.Name;
            d_info.Info = $"Do you want buy {tileInfo.Name}\n for {tileInfo.Price}?";
            d_info.Button1 = "Yes";
            d_info.Button2 = "No";
        }
        return d_info;
    }
}
