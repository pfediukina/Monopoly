using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI playerMoney;
    [SerializeField] private TextMeshProUGUI propertyList;


    public void ChangePlayerMoneyTextWithValue(int start_money, int change_val, int current_money)
    {
        playerMoney.text = $"{start_money}\n{(change_val > 0 ?" +" : "")}{change_val}\n{current_money}";
    }

    public void UpdatePlayerMoney(int current_money)
    {
        playerMoney.text = $"{current_money}";
    }

    public void SetTextColor(Color color)
    {
        Color c = color;
        c.a = 1;
        playerMoney.color = c;
    }

    public void UpdatePlayerProperty(List<Tile> tiles)
    {
        string props = "";
        foreach(Tile t in tiles)
        {
            string color = ColorUtility.ToHtmlStringRGB(t.GetTileInfo().Color);
            props += $"<color=#{color}> ? </color> {t.GetTileInfo().Name}\n";
        }
        propertyList.text = props;
    }
}
