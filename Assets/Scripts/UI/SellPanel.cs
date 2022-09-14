using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private GameManager gameManager;

    [Header("Components")]
    [SerializeField] private Transform contentTransform;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button loseButton;
    [SerializeField] private TextMeshProUGUI debtText;
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Settings")]
    [SerializeField] private GameObject example;

    

    private List<SellElement> _sellElements = new List<SellElement>();
    private int _debt;
    private int _money;

    private void Start()
    {
        ShowPanel(false);
        sellButton.onClick.AddListener(delegate
            {
                gameManager.PlayerChoseButton(DialogResult.SELL);
            });
        loseButton.onClick.AddListener(delegate
            {
                gameManager.PlayerChoseButton(DialogResult.LOSE);
            });
    }

    public void AddElementToList(string name, int price, Tile tile)
    {
        GameObject createdElement = Instantiate(example, contentTransform);

        SellElement sellElement = new SellElement(createdElement.GetComponentInChildren<Toggle>(), 
            createdElement.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
            createdElement.transform.Find("Price").GetComponent<TextMeshProUGUI>());

        sellElement.name.SetText(name);
        sellElement.price.SetText(price.ToString());

        sellElement.tile = tile;

        _sellElements.Add(sellElement);
        sellElement.toggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChanged(sellElement);
        });
    }

    public void SetDept(int debt)
    {
        _debt = debt;
        debtText.SetText($"DEPT: {_debt}"); 
        debtText.color = Color.red;
    }

    public void ShowPanel(bool active)
    {
        for (int i = contentTransform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
            
        }
        _sellElements.Clear();
        gameObject.SetActive(active);
    }

    private void ToggleValueChanged(SellElement element)
    {
        int price = element.GetPrice();
        if (element.toggle.isOn)
        {
            _money += price;
            _debt += price;
        }
        else
        {
            _money -= price;
            _debt -= price;
        }
        moneyText.SetText($"MONEY: {_money}");
        debtText.SetText($"DEPT: {_debt}");
        if (_debt > 0)
        {
            sellButton.interactable = true;
            debtText.color = Color.green;
        }
        else
        {
            sellButton.interactable = false;
            debtText.color = Color.red;
        }
    }
    public List<Tile> GetToggledElements ()
    {
        List<Tile> names = new List<Tile>();
        foreach(var item in _sellElements)
        {
            if(item.toggle.isOn)
            {
                names.Add(item.tile);
            }
        }
        return names;
    }
}
