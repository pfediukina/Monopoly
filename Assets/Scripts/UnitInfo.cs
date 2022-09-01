using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitInfo", menuName = "Info/Unit", order = 2)]
public class UnitInfo : ScriptableObject
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public int position;
}
