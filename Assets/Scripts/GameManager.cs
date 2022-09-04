using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public UnitController player1;

    private UIManager _UI;
    private UnitController _movingPlayer;

    //-----------------
    GameObject dot;

    void Start()
    {
        _movingPlayer = player1;
        board.Init();
        player1.Init(board);
        _UI = transform.GetComponent<UIManager>();
    }

    public void MoveCurrentPlayer(int step)
    {
        _UI.HideInfoPanel();

        if (dot != null)
            Destroy(dot);
        dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dot.transform.position = _movingPlayer.transform.position;

        _movingPlayer.Move(step);
        TileInfo tileInfo = board.GetTile(_movingPlayer.GetUnitInfo().position).tileInfo;

        if (tileInfo.tileType == TileType.Colored)
        {
            string desc = $"Do you want to buy it for {tileInfo.Price}?";
            _UI.ShowInfoPanel(tileInfo.Name, desc);
        }
    }

    public void PlayerChoseButton(bool result)
    {
        Tile tile = board.GetTile(_movingPlayer.GetUnitInfo().position);
        if (result)
            tile.SetPlayer(_movingPlayer);
        else
            tile.SetPlayer(null);
    }
}
