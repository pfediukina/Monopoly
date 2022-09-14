using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Button rollButton;
    public TextMeshPro rollText;
    public InfoPanel infoPanel;
    public TextMeshProUGUI moneyText;
    //===============================
    public TMP_InputField testStep;

    [SerializeField] private SellPanel sellPanel;


    private GameManager _gameManager;
    private int _rollNum1;
    private int _rollNum2;

    public void Start()
    {
        _gameManager = transform.GetComponent<GameManager>();
        rollButton.onClick.AddListener(Roll);
    }

    void Roll()
    {
        _rollNum1 = Random.Range(1, 7);
        _rollNum2 = Random.Range(1, 7);

        rollText.SetText($"{_rollNum1.ToString()} {_rollNum2.ToString()}");
        _gameManager.MoveCurrentPlayer(_rollNum1 + _rollNum2);
    }

    public void ShowInfoPanel(string title, string desc, bool onlyInfo = false)
    {
        infoPanel.SetText(title, desc);
        rollButton.interactable = false;
        if(onlyInfo)
            infoPanel.ShowPanel(true, true);
        else
            infoPanel.ShowPanel(true, false);
    }
    public void HideInfoPanel()
    {
        infoPanel.ShowPanel(false);
        rollButton.interactable = true;
    }

    public void PlayerChoseButton(DialogResult result)
    {
        HideInfoPanel();
        _gameManager.PlayerChoseButton(result);
    }

    public void UpdatePlayerMoney(UnitController player)
    {
        moneyText.SetText(player.GetUnitInfo().money.ToString());
        moneyText.color = player.GetUnitInfo().color;
    }

    public void SetPlayerMoneyUI(int money, int rent, Color color, bool plus = false)
    {
        //moneyText.color = color;
        if (rent == 0)
            moneyText.SetText(money.ToString());
        else
        {
            moneyText.SetText($"{(plus ? money - rent : money + rent)}\n{(plus ? "+" : "-")}{Mathf.Abs(rent)}\n{money}");
        }
    }

    public void ShowBuyDialig(TileInfo tileInfo)
    {
        string desc = $"Do you want to buy {tileInfo.Name} for {tileInfo.Price}?";
        ShowInfoPanel(tileInfo.Name, desc, false);
    }
    public void ShowRentDialig(TileInfo tileInfo, RentInfo rentInfo, UnitController _movingPlayer)
    {
        string desc;
        if(rentInfo.count == 0)
            desc = $"You payed the rent: {rentInfo.count}";
        else
            desc = $"You payed the rent: {rentInfo.rent}*{rentInfo.count}";
        ShowInfoPanel(tileInfo.Name, desc, true);
        SetPlayerMoneyUI(_movingPlayer.GetUnitInfo().money, rentInfo.rent, _movingPlayer.GetUnitInfo().color);
    }

    public SellPanel GetSellPanel()
    {
        return sellPanel;
    }
}
