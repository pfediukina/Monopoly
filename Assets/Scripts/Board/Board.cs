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

    private List<Tile> _tiles = new List<Tile>();
    private List<GameObject> _sides = new List<GameObject>();

    public void Update()
    {
        if(update)
        {
            update = false;
            Init();
        }    
    }

    public void Init()
    {
        ClearBoard();
        _tiles.Clear();
        int bite_offset = 0;
        float half_size = GetSideSize() / 2;

        Vector3 angles = new Vector3(0, 0, 0);
        for (int j = 0; j < 4; j++)
        {
            Vector3 start_pos = new Vector3(half_size, 0, half_size);
            GameObject side = new GameObject();
            side.transform.parent = transform;

            _sides.Add(side);

            for (int i = 0; i < boardInfo.sideSize; i++)
            {
                CreateTile(boardInfo.tileInfos[_tiles.Count % boardInfo.tileInfos.Count], side.transform);
            }

            side.transform.eulerAngles = angles;

            start_pos.x *= boardInfo.biteCodeOfSidePos[bite_offset++] == '0' ? -1 : 1;
            start_pos.z *= boardInfo.biteCodeOfSidePos[bite_offset++] == '0' ? -1 : 1;

            side.transform.position = start_pos;
            angles.y += 90;
        }
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
        Vector3 position = new Vector3(0, 0, 0);
        if (_tiles.Count %10 != 0)
        {
            position = _tiles[_tiles.Count - 1].transform.position;
            position.x += (_tiles[_tiles.Count - 1].transform.localScale.x / 2) + (tile_object.transform.localScale.x / 2);
            
        }
        tile_object.transform.position = position;
        Tile tile = tile_object.GetComponent<Tile>();
        _tiles.Add(tile);
        tile.SetTileInfo(info);
        tile.Init();
    }

    public List<Tile> GetAllTiles()
    {
        return new List<Tile>(_tiles);
    }

    private float GetSideSize()
    {
        float result = 0.0f;
        result = (boardInfo.sideSize - 1) * normalPrefab.transform.localScale.x + cornerPrefab.transform.localScale.x;
        return result;
    }

    private void ClearBoard()
    {
        foreach (var child in _sides)
        {
            DestroyImmediate(child.gameObject);
        }
    }


}
