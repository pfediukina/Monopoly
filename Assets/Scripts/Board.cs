using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Board : MonoBehaviour
{
    public GameObject board;
    public Tile cornerTilePref;
    public Tile tilePref;

    public bool active;

    [SerializeField]
    private BoardInfo _boardInfo;
    private List<Tile> _tileObjects = new List<Tile>();

    private void Update()
    {
        if(active)
        {
            Init();
            active = false;
        }    
    }

    public void Init()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        _tileObjects.Clear();
        GenerateBoard();
    }

    public Vector3 GetTileCenter(int index)
    {
        var i = index;
        if (index < 0 || index >= _tileObjects.Count)
        {
            i = Mathf.Abs(index) % 40;
        }
        var scr = GetTile(i).transform.position + Vector3.up * 0.5f;
        return scr;
    }
   
    public Tile GetTile(int id)
    {
        var i = id;
        if (id < 0 || id >= _tileObjects.Count)
        {
            i = Mathf.Abs(id) % 40;
        }
        foreach (var tile in _tileObjects)
        {

            if (tile.tileInfo.ID == i) return tile;
        }
        return null;
    }
    public List<Tile> GetTiles()
    {
        var list = new List<Tile>(_tileObjects);
        return list;
    }

    private void GenerateBoard()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        Vector3 offset = new Vector3(tilePref.transform.localScale.x, 0, 0);
        int[] specials = { 1, 4 };
        Vector3 rot = new Vector3(0, 0, 0);


        _tileObjects.Add(
            CreateTile(TileType.Corner, Vector3.zero, Vector3.zero));

        pos.x += cornerTilePref.transform.localScale.x - 0.25f;
        CreateBoardSide(pos, offset, specials, rot);

        pos.x += 9*tilePref.transform.localScale.x + 0.25f;
        _tileObjects.Add(
            CreateTile(TileType.Corner, pos, new Vector3(0, 90, 0)));

        pos.z -= cornerTilePref.transform.localScale.x - 0.25f;
        rot.y = 90;
        offset.x = 0;
        offset.z = tilePref.transform.localScale.x * -1;
        CreateBoardSide(pos, offset, specials, rot);

        pos.z += (9 * tilePref.transform.localScale.x + 0.25f) * -1;
        _tileObjects.Add(
            CreateTile(TileType.Corner, pos, new Vector3(0, 180, 0)));

        pos.x -= cornerTilePref.transform.localScale.x - 0.25f;
        rot.y = 180;
        offset.x = -tilePref.transform.localScale.x;
        offset.z = 0;
        CreateBoardSide(pos, offset, specials, rot);

        pos.x = 0;
        _tileObjects.Add(
            CreateTile(TileType.Corner, pos, new Vector3(0, 270, 0)));

        pos.z += cornerTilePref.transform.localScale.x - 0.25f;
        rot.y = 270;
        offset.x = 0;
        offset.z = tilePref.transform.localScale.x;
        CreateBoardSide(pos, offset, specials, rot);

        
    }

    private void CreateBoardSide(Vector3 startPos, Vector3 offset, int[] specials, Vector3 rot)
    {
        var pos = startPos;
        bool isSpecial = false;
        for (int i = 0; i < 9; i++)
        {
            isSpecial = false;
            for (int j = 0; j < specials.Length; j++)
            {
                if (i == specials[j])
                {
                    isSpecial = true;
                    break;
                }
            }

            if(isSpecial)
            {
                _tileObjects.Add(
                    CreateTile(TileType.Special, pos, rot));
            }
            else
            {
                if(i < 4)
                    _tileObjects.Add(
                        CreateTile(TileType.Colored, pos, rot));
                else
                    _tileObjects.Add(
                        CreateTile(TileType.Colored, pos, rot));
            }
            pos += offset;
        }
    }

    private Tile CreateTile(TileType type, Vector3 pos, Vector3 rot)
    {
        Tile tile;
        if(type == TileType.Corner)
            tile = Instantiate(cornerTilePref, board.transform);
        else
            tile = Instantiate(tilePref, board.transform);

        tile.transform.position = pos;
        tile.transform.Rotate(rot);
        var scr = tile.GetComponent<Tile>();

        var tileInfo = _boardInfo.GetTileByID(_tileObjects.Count);

        if(tileInfo != null)
            scr.SetTileInfo(tileInfo);

        return tile;
    }
}
