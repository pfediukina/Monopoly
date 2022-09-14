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
    public Button ok;

    private DialogResult _clickedButton;

    public void Start()
    {
        ShowPanel(false);
        //yes.onClick.AddListener(YesPressed);
        yes.onClick.AddListener(delegate
        {
            UI.PlayerChoseButton(DialogResult.YES);
        });
        no.onClick.AddListener(delegate
        {
            UI.PlayerChoseButton(DialogResult.NO);
        });
        ok.onClick.AddListener(delegate
        {
            UI.PlayerChoseButton(DialogResult.OKEY);
        });
    }

    public void ShowPanel(bool show, bool onlyInfo = false)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
        if (onlyInfo && show)
        {
            yes.gameObject.SetActive(false);
            no.gameObject.SetActive(false);
            ok.gameObject.SetActive(true);
        }
        else if (!onlyInfo && show)
        {
            yes.gameObject.SetActive(true);
            no.gameObject.SetActive(true);
            ok.gameObject.SetActive(false);
        }
    }

    public void SetText(string title, string desc)
    {
        titleTMP.SetText(title);
        descTMP.SetText(desc);
    }

    public void ReturnClick() { UI.PlayerChoseButton(_clickedButton); }
        
}
