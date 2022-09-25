using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitInfo", menuName = "Info/Unit Info", order = 3)]
public class UnitInfo : ScriptableObject
{
    [SerializeField] public int ID;
    [SerializeField] public int Position;
    [SerializeField] public int Jail;
    [SerializeField] public int Money;
    [SerializeField] public Color Color;
    [SerializeField] public Vector3 PosOffset;
}
