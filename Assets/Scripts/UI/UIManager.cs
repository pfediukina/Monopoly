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

    public void ShowInfoPanel(string title, string desc)
    {
        infoPanel.SetText(title, desc);
        infoPanel.ShowPanel(true);
        rollButton.interactable = false;
    }
    public void HideInfoPanel()
    {
        infoPanel.ShowPanel(false);
    }

    public void PlayerChoseButton(bool result)
    {
        HideInfoPanel();
        rollButton.interactable = true;
        _gameManager.PlayerChoseButton(result);
    }
}
