using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardInfo", menuName = "Info/Cards", order = 3)]
public class CardInfo : ScriptableObject
{
    public List<ChanceInfo> chanceInfo;
    public List<ChanceInfo> chestInfo;
}
