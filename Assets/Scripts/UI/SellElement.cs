using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellElement
{
    public Toggle toggle;
    public TextMeshProUGUI name;
    public TextMeshProUGUI price;

    public Tile tile;

    public SellElement(Toggle toggle, TextMeshProUGUI name, TextMeshProUGUI price)
    {
        this.toggle = toggle;
        this.name = name;
        this.price = price;
    }

    public int GetPrice()
    {
        int resPrice;
        int.TryParse(price.text, out resPrice);
        return resPrice;
    }
}
