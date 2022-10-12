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
    [SerializeField] private Camera unitCamera;

    [Header("Test")]
    [SerializeField] private int playerPos = -1;
    [SerializeField] private bool update = false;


    void Update()
    {
        if(GameSettings.testMode && update)
        {
            update = false;
            MovePlayerToTile(playerPos);
            Init();
        }
    }

    public UnitInfo GetPlayerInfo() => unitInfo;

    public void Init()
    {
        SetPlayerColor();
        ResetPlayerInfo();
        MovePlayerToTile(0);
    }

    public void SetPlayerColor()
    {
        var tempMaterial = new Material(unitMesh.GetComponent<Renderer>().sharedMaterial);
        tempMaterial.color = unitInfo.Color;
        unitMesh.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    }

    public void MovePlayerToTile(int tileID)
    {
        BaseTile tile = board.GetTileByID(tileID);
        Vector3 tile_pos = tile.transform.position;
        tile_pos.y = transform.position.y;
        transform.position = tile_pos + unitInfo.PosOffset;
        unitInfo.Position = tile.tileInfo.ID;
        Vector3 player_rot = new Vector3(0, 0, 0);
        player_rot.y = (unitInfo.Position / 10) * 90;
        transform.eulerAngles = player_rot;
    }

    public void MovePlayerWithStep(int step)
    {
        if (board.GetAllTiles().Count == 0) return;
        int new_pos = (unitInfo.Position + step) % board.GetAllTiles().Count;
        MovePlayerToTile(new_pos);
    }

    public void SetPlayerActive(bool active)
    {
        unitCamera.gameObject.SetActive(active);
    }

    public bool AddPlayerMoney(int value)
    {
        int new_money = unitInfo.Money += value;
        if (new_money <= 0) return false;
        unitInfo.Money = new_money;
        return true;
    }

    public void HidePlayer()
    {
        unitMesh.gameObject.SetActive(false);
    }

    private void ResetPlayerInfo()
    {
        unitInfo.Jail = examplaryInfo.Jail;
        unitInfo.Money = examplaryInfo.Money;
    }

}