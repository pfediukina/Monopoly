using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
     Corner,
     Special,
     Colored
}

public enum TileColor
{
    Red,
    Yellow,
    Green,
    Blue,
    Brown,
    Grey,
    Pink,
    Orange
}

public enum SpecialTile
{
    Start, 
    Jail, 
    Tax,
    Station,
    Chance,
    CommunityChest,
    Company
}

public struct RentInfo
{
    public int rent;
    public int count;

    public RentInfo(int _rent, int _count)
    {
        rent = _rent;
        count = _count;
    }
    public void SetRent(int _rent, int _count)
    {
        rent = _rent;
        count = _count;
    }
}
