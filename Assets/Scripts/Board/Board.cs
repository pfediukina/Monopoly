using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Board : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private BoardInfo boardInfo;

    [Header("Components")]
    [SerializeField] private GameObject normalPrefab;
    [SerializeField] private GameObject cornerPrefab;

    [Header("Test Mode")]
    [SerializeField] private bool update;

    private List<BaseTile> _tiles = new List<BaseTile>();
    private List<GameObject> _sides = new List<GameObject>();


    public void Update()
    {
        if(GameSettings.testMode)
        {
            if (update)
            {
                update = false;
                BuildBoard();
            }
        }
    }

    public void BuildBoard()
    {
        ClearBoard();

        int bite_offset = 0;
        Vector3 angles = new Vector3(0, 0, 0);

        for (int j = 0; j < 4; j++) // build side
        {
            CreateSide(bite_offset, angles);
            angles.y += 90;
            bite_offset += 2;
        }

        CreateCentralCube();
    }


    public List<BaseTile> GetAllTiles() => _tiles;

    public BaseTile GetTileByID(int tileID) => _tiles[tileID];


    private void CreateSide(int bite_offset, Vector3 angles)
    {
        float half_size = GetSideSize() / 2;
        Vector3 start_pos = new Vector3(half_size, 0, half_size);
        GameObject side = new GameObject();
        side.transform.parent = transform;

        _sides.Add(side);

        for (int i = 0; i < boardInfo.sideSize; i++)
        {
            CreateTile(boardInfo.tileInfos[_tiles.Count % boardInfo.tileInfos.Count], side.transform);
        }

        side.transform.eulerAngles = angles;

        start_pos.x *= boardInfo.biteCodeOfSidePos[bite_offset] == '0' ? -1 : 1;
        start_pos.z *= boardInfo.biteCodeOfSidePos[bite_offset + 1] == '0' ? -1 : 1;

        side.transform.position = start_pos;
    }

    private void CreateCentralCube()
    {
        var central_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        central_cube.transform.position = Vector3.zero;
        central_cube.transform.parent = transform;
        central_cube.transform.localScale = new Vector3(GetSideSize() - cornerPrefab.transform.localScale.x,
                                                        Vector3.up.y, GetSideSize() - cornerPrefab.transform.localScale.x);
        _sides.Add(central_cube);
    }

    private void CreateTile(TileInfo info, Transform parent)
    {
        GameObject tile_object;
        if (info.Type == TileType.Start || info.Type == TileType.CornerTax || info.Type == TileType.Jail)
            tile_object = Instantiate(cornerPrefab, parent);
        else
            tile_object = Instantiate(normalPrefab, parent);

        BaseTile tile;
        switch (info.Type)
        {
            case TileType.Colored:
                tile = tile_object.AddComponent<ColoredTile>();
                break;
            case TileType.Station:
                tile = tile_object.AddComponent<StationTile>();
                break;
            case TileType.Company:
                tile = tile_object.AddComponent<StationTile>();
                break;
            case TileType.Chance:
            case TileType.Chest:
                tile = tile_object.AddComponent<CardTile>();
                break;
            case TileType.Start:
                tile = tile_object.AddComponent<StartTile>();
                break;
            case TileType.CornerTax:
                tile = tile_object.AddComponent<CornerTaxTile>();
                break;
            case TileType.Tax:
                tile = tile_object.AddComponent<TaxTile>();
                break;
            case TileType.Jail:
                tile = tile_object.AddComponent<JailTile>();
                break;
            default:
                tile = tile_object.AddComponent<ColoredTile>();
                break;
        }

        Vector3 position = new Vector3(0, 0, 0);
        if (_tiles.Count % 10 != 0)
        {
            position = _tiles[_tiles.Count - 1].transform.position;
            position.x += (_tiles[_tiles.Count - 1].transform.localScale.x / 2) + (tile_object.transform.localScale.x / 2);

        }
        _tiles.Add(tile);
        tile.transform.position = position;
        tile.tileInfo = info;
        tile.Init();
    }

    private float GetSideSize()
    {
        float result = 0.0f;
        result = (boardInfo.sideSize - 1) * normalPrefab.transform.localScale.x + cornerPrefab.transform.localScale.x;
        return result;
    }

    private void ClearBoard()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        _tiles.Clear();
        _sides.Clear();
    }
}
