using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : BaseDialog
{
    [Header("Components")]
    [SerializeField] public TextMeshProUGUI titleText;
    [SerializeField] public TextMeshProUGUI descText;
    [SerializeField] public TextMeshProUGUI button1Text;
    [SerializeField] public TextMeshProUGUI button2Text;
    [SerializeField] public Button button1;
    [SerializeField] public Button button2;

    public override void Init()
    {
        AddButtonsEvents();
    }

    public override void OnDialogResponce(bool response)
    {

    }

    public override void SetDialogInfo(DialogInfo info)
    {
        titleText.text = info.Title;
        descText.text = info.Desc;
        button1Text.text = info.Button1;
        button2Text.text = info.Button2;
    }

    private void AddButtonsEvents()
    {
        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(delegate
        {
            OnDialogResponce(true);
        });
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(delegate
        {
            OnDialogResponce(false);
        });
    }
}
