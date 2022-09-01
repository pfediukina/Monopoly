using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileInfo", menuName = "Info/Tile", order = 1)]
public class TileInfo : ScriptableObject
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string Name;
    [SerializeField]
    public TileType tileType;
    [SerializeField]
    public TileColor Color;
    [SerializeField]   
    public bool HasPrice = true;
    [SerializeField]
    public int Price;
}
