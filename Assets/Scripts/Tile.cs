using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Tile : MonoBehaviour
{
    public GameObject colorMesh;
    public TextMeshPro nameText;
    public TextMeshPro priceText;
    public GameObject playerCube;
    public TileInfo tileInfo;

    public void SetPlayer(UnitController player)
    {
        if(player == null)
        {
            tileInfo.owningPlayer = null;
            ShowPlayerCube(false);
            return;
        }
        tileInfo.owningPlayer = player;
        ShowPlayerCube(true);
    }

    private void ShowPlayerCube(bool active) //private in future (called from SetPlayer())
    {
        if (playerCube == null)
        {
            return;
        }
        playerCube.gameObject.SetActive(active);
        if (active)
        {
            var tempMaterial = new Material(playerCube.GetComponent<Renderer>().sharedMaterial);
            Color color = tileInfo.owningPlayer.GetUnitInfo().color;
            tempMaterial.color = color;
            playerCube.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
    }

    public void SetTileInfo(TileInfo info){
        if (info == null) return;
        tileInfo = info;
        SetType();
    }

    public Vector3 GetPlayerVectorPos(UnitController player)
    {
        Vector3 pos;
        pos = transform.position;
        pos.y = 0.5f;
        switch(player.ID)
        {
            case 0: pos.x -= 0.4f;  break;
            case 1: pos.x += 0.4f; break;
            case 2: pos.z += 0.4f; break;
            case 3: pos.z -= 0.4f; break;
        }
        return pos;
    }

    public RentInfo GetRent(Board board)
    {
        int owning = 0;

        owning = board.GetTiles().Where(p => (p.tileInfo.tileType == TileType.Colored && 
                                    p.tileInfo.owningPlayer == tileInfo.owningPlayer && 
                                    p.tileInfo.owningPlayer != null &&
                                    p.tileInfo.Color == tileInfo.Color)).Count();

        //for(int i = tileInfo.ID - 4; i < tileInfo.ID + 4; i++)
        //{
        //    if(i < 0 || i >= 40) continue;
        //    if (tileInfo.Color == board.GetTile(i).tileInfo.Color && board.GetTile(i).tileInfo.tileType == TileType.Colored)
        //        if (tileInfo.owningPlayer == board.GetTile(i).tileInfo.owningPlayer)
        //            owning++;
        //}
        int rent = tileInfo.Price / 10 * (owning + 2);
        RentInfo rentInfo = new RentInfo(rent, owning);
        return rentInfo;

    }

    private void SetType()
    {
        SetTexts();

        if (colorMesh == null) return;
        if(tileInfo.tileType == TileType.Special)
        {
            colorMesh.SetActive(false);
        }
        else
            SetColor();

        
    }

    private void SetTexts()
    {
        if (nameText != null)
            nameText.SetText(tileInfo.Name);

        if (priceText != null && tileInfo.HasPrice)
        {
            priceText.gameObject.SetActive(true);
            priceText.SetText($"{Mathf.Abs(tileInfo.Price)}");
        }

        if (tileInfo.tileType == TileType.Special)
        {
            Vector3 pos = nameText.transform.localPosition;
            pos.z -= 0.2f;
            nameText.transform.localPosition = pos;
        }
    }

    private void SetColor()
    {
        if (colorMesh == null) return;
        var tempMaterial = new Material(colorMesh.GetComponent<Renderer>().sharedMaterial);
        Color color;
        switch (tileInfo.Color)
        {
            case TileColor.Red:
                ColorUtility.TryParseHtmlString("#FF0000FF", out color); break;
            case TileColor.Yellow:
                ColorUtility.TryParseHtmlString("#FFFF00FF", out color); break;
            case TileColor.Green:
                ColorUtility.TryParseHtmlString("#32CD32FF", out color); break;
            case TileColor.Blue:
                ColorUtility.TryParseHtmlString("#00BFFFFF", out color); break;
            case TileColor.Brown:
                ColorUtility.TryParseHtmlString("#8B4513FF", out color); break;
            case TileColor.Grey:
                ColorUtility.TryParseHtmlString("#808080FF", out color); break;
            case TileColor.Pink:
                ColorUtility.TryParseHtmlString("#FF1493FF", out color); break;
            case TileColor.Orange:
                ColorUtility.TryParseHtmlString("#FF8C00FF", out color); break;
            default:
                ColorUtility.TryParseHtmlString("#FFFFFFFF", out color); break;
        }
        tempMaterial.color = color;
        colorMesh.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    }

}
