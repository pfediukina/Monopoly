using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject colorMesh;
    [SerializeField] private GameObject ownerMesh;

    [Header("Settings")]
    [SerializeField] private float nameOffset;

    public void SetTileName(string name, bool is_colored)
    {
        if (nameText == null) return;

        nameText.text = name;
        if (!is_colored)
        {
            Vector3 name_pos = nameText.transform.localPosition;
            name_pos.z += nameOffset;
            nameText.transform.localPosition = name_pos;
        }
    }

    public void SetTilePrice(int price)
    {
        if (priceText == null) return;
        priceText.gameObject.SetActive(true);
        priceText.SetText(Mathf.Abs(price).ToString());
    }

    public void SetTileColor(Color color, bool is_colored = true) 
    {
        if (colorMesh == null) return;

        if (!is_colored)
        {
            colorMesh.SetActive(false);
            return;
        }
        colorMesh.SetActive(true);
        var tempMaterial = new Material(colorMesh.GetComponent<Renderer>().sharedMaterial);
        tempMaterial.color = color;
        colorMesh.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    }
    
    public void HideOwnerColorMesh()
    {
        ownerMesh.SetActive(false);
    }

    public void SetTileOwnerColor(Color color) // null if nobody
    {
        if (ownerMesh == null) return;

        ownerMesh.SetActive(true);
        var tempMaterial = new Material(ownerMesh.GetComponent<Renderer>().sharedMaterial);
        tempMaterial.color = color;
        ownerMesh.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    }

}
