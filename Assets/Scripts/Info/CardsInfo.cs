using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsInfo", menuName = "Info/Cards Info", order = 5)]
public class CardsInfo : ScriptableObject
{
    [SerializeField] public List<CardInfo> Chances;
    [SerializeField] public List<CardInfo> Chests;

}

[Serializable]
public class CardInfo
{
    [SerializeField] public string Desc;
    [SerializeField] public int Value;
    [SerializeField] public CardType Type;
}

public enum CardType
{
    Position, 
    Valuable
}
