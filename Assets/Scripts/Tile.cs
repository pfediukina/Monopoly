using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            ShowPlayerCube(false);
            return;
        }
        ShowPlayerCube(true);
    }

    private void ShowPlayerCube(bool active) //private in future (called from SetPlayer())
    {
        playerCube.gameObject.SetActive(active);
    }

    public void SetTileInfo(TileInfo info){
        if (info == null) return;
        tileInfo = info;
        SetType();
    }


    private void SetType()
    {
        if (colorMesh == null) return;
        if(tileInfo.tileType == TileType.Special)
        {
            colorMesh.SetActive(false);
        }
        else
            SetColor();

        SetTexts();
        
    }

    private void SetTexts()
    {
        if (nameText != null)
            nameText.SetText(tileInfo.Name);
        if (priceText != null && tileInfo.HasPrice)
        {
            priceText.gameObject.SetActive(true);
            priceText.SetText($"{Mathf.Abs(tileInfo.Price)}$");
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
