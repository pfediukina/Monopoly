using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitInfo", menuName = "Info/Unit", order = 2)]
public class UnitInfo : ScriptableObject
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public int position = 0;
    [SerializeField]
    public int money = 1500;
    public float offset;
    public Color color;
    public int jail;
}
