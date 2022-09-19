using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBuilder: MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasGroup messageBox;

    [SerializeField] private TextMeshProUGUI captionText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI button1;
    [SerializeField] private TextMeshProUGUI button2;

    public void BuildMessageDialog( string caption, string info, string button_1, string button_2)
    {
        messageBox.alpha = 1;
        captionText.text = caption;
        infoText.text = info;
        button1.text = button_1;
        button2.text = button_2;

    }

    public void HideMessageDialog()
    {
        messageBox.alpha = 0;
    }

}

public struct DialogInfo
{
    public DialogID ID;
    public string Caption;
    public string Info;
    public string Button1;
    public string Button2;
}
