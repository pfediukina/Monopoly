using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ListBoxUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIManager UI;

    [Header("Components")]
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Transform listTransform;
    [SerializeField] private TextMeshProUGUI doubtText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private GameObject itemPrefab;

    private List<ListElement> _listOfElements = new List<ListElement>();
    private bool is_doubt = false;
    private int _currentMoney = 0;
    private int _currentDoubt = 1;

    void Start()
    {
        button1.onClick.AddListener(delegate { UI.OnPlayerPressedButton(true); });
        button2.onClick.AddListener(delegate { UI.OnPlayerPressedButton(false); });
    }


    public void SetListActive(bool active)
    {
        canvas.alpha = active ? 1 : 0;
        canvas.blocksRaycasts = active;
    }

    public void SetList(List<Tile> list)
    {
        CleanList();
        foreach(Tile item in list)
        {
            ListElement temp = new ListElement();
            temp.tile = item;
            temp.obj = Instantiate(itemPrefab);
            temp.obj.transform.parent = listTransform;
            temp.obj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.GetTileInfo().Name;
            temp.obj.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = (item.GetTileInfo().Price / 2).ToString();
            temp.toggle = temp.obj.transform.GetComponentInChildren<Toggle>();
            temp.toggle.onValueChanged.AddListener(delegate
            {
                ToggleEvent(temp);
            });

            _listOfElements.Add(temp);
        }
    }

    private void ToggleEvent(ListElement element)
    {
        if (element.toggle.isOn)
        {
            _currentMoney += element.tile.GetTileInfo().Price / 2;
            _currentDoubt += element.tile.GetTileInfo().Price / 2;
        }
        else
        {
            _currentMoney -= element.tile.GetTileInfo().Price / 2;
            _currentDoubt -= element.tile.GetTileInfo().Price / 2;
        }
        UpdateDoubtAndMoney();
    }
    
    public void ChangeDoubtAndMoney(int doubt, int money)
    {
        _currentMoney = money;
        _currentDoubt = doubt;
        UpdateDoubtAndMoney();
    }

    public void ChangeDoubtAndMoney(int money)
    {
        _currentMoney = money;
        doubtText.text = "";
        UpdateDoubtAndMoney();
    }

    public void UpdateDoubtAndMoney()
    {
        moneyText.text  = "MONEY " + _currentMoney.ToString();
        if(is_doubt)
            doubtText.text = "DOUBT " + _currentDoubt.ToString();
        if (_currentDoubt > 0)
        {
            doubtText.color = Color.green;
            button1.interactable = true;
        }
        else
        {
            doubtText.color = Color.red;
            if(is_doubt)
                button1.interactable = false;
        }
    }

    public void CleanList()
    {
        foreach(var i in _listOfElements)
        {
            Destroy(i.obj);
        }
        _listOfElements.Clear();
    }

    public List<Tile> GetCheckedList()
    {
        List<Tile> tiles = new List<Tile>();
        foreach(var item in _listOfElements)
        {
            if(item.toggle.isOn)
            {
                tiles.Add(item.tile);
            }
        }
        return tiles;
    }
}
