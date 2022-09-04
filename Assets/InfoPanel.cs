using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public UIManager UI;

    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI descTMP;

    public Button yes;
    public Button no;

    private bool _clickedButton;

    public void Start()
    {
        ShowPanel(false);
        yes.onClick.AddListener(YesPressed);
        no.onClick.AddListener(NoPressed);
    }

    public void ShowPanel(bool show)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }

    public void SetText(string title, string desc)
    {
        titleTMP.SetText(title);
        descTMP.SetText(desc);
    }

    public void ReturnClick() { UI.PlayerChoseButton(_clickedButton); }
        
    private void YesPressed() { _clickedButton = true; ReturnClick(); }
    private void NoPressed() { _clickedButton = false; ReturnClick(); }
}
