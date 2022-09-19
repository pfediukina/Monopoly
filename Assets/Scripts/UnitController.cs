using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UnitController : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private UnitInfo unitInfo;
    [SerializeField] private UnitInfo examplaryInfo;
    [SerializeField] private Board board;

    [Header("Components")]
    [SerializeField] private GameObject unitMesh;

    //[Header("Test")]
    //[SerializeField] private int playerPos = -1;
    //[SerializeField] private bool update = false;




    void Update()
    {
        //if (playerPos != -1)
        //{
        //    MovePlayerWithStep(playerPos);
        //    playerPos = -1;
        //}   
        //if(update)
        //{
        //    update = false;
        //    Init();
        //}    
    }

    public void Init()
    {

        var tempMaterial = new Material(unitMesh.GetComponent<Renderer>().sharedMaterial);
        tempMaterial.color = unitInfo.Color;
        unitMesh.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        ResetPlayerInfo();

        MovePlayerToTile(board.GetAllTiles()[0]); // reset player position
    }

    public UnitInfo GetPlayerInfo()
    {
        return unitInfo;
    }

    public void MovePlayerToTile(Tile tile)
    {
        Vector3 tile_pos = tile.transform.position;
        tile_pos.y = transform.position.y;
        transform.position = tile_pos + unitInfo.PosOffset;
        unitInfo.Position = tile.GetTileInfo().ID;
        Vector3 player_rot = new Vector3(0, 0, 0);
        player_rot.y = (unitInfo.Position / 10) * 90;
        transform.eulerAngles = player_rot;
    }

    public void MovePlayerWithStep(int step)
    {
        if (board.GetAllTiles().Count == 0) return;
        int new_pos = (unitInfo.Position + step) % board.GetAllTiles().Count;
        Debug.Log(unitInfo.Position + step + "/" + new_pos);
        Tile new_tile = board.GetAllTiles()[new_pos];
        MovePlayerToTile(new_tile);
    }

    private void ResetPlayerInfo()
    {
        //unitInfo.Position = examplaryInfo.Position;
    }
}