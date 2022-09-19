using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardInfo", menuName = "Info/Board Info", order = 1)]
public class BoardInfo : ScriptableObject
{
    [SerializeField] public int sideSize;
    [SerializeField] public int sides;
    [SerializeField] public List<TileInfo> tileInfos;
    [SerializeField] public string biteCodeOfSidePos;
}
