using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileInfo", menuName = "Info/Tile Info", order = 2)]
public class TileInfo : ScriptableObject
{
    [SerializeField] public int ID;
    [SerializeField] public string Name;
    [SerializeField] public TileType Type;
    [SerializeField] public int Price;
    [SerializeField] public Color Color;
    [SerializeField] public UnitController Owner;
    [SerializeField] public int[] Rent;
}
