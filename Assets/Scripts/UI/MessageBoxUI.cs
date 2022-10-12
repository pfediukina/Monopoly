using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBoxUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIManager UI;

    [Header("Components")]
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private TextMeshProUGUI captionText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;

    private TextMeshProUGUI _buttonText1;
    private TextMeshProUGUI _buttonText2;

    //void Start()
    //{
    //    _buttonText1 = button1.GetComponentInChildren<TextMeshProUGUI>();
    //    _buttonText2 = button2.GetComponentInChildren<TextMeshProUGUI>();

    //    button1.onClick.AddListener(delegate { UI.OnPlayerPressedButton(true); });
    //    button2.onClick.AddListener(delegate { UI.OnPlayerPressedButton(false); });
    //}

    //public void SetMessageActive(bool active)
    //{
    //    canvas.alpha = active == true ? 1 : 0;
    //    canvas.blocksRaycasts = active;
    //}

    //public void BuildMessageBox(string caption, string info, string but1)
    //{
    //    button2.gameObject.SetActive(false);
    //    captionText.text = caption;
    //    infoText.text = info;
    //    _buttonText1.text = but1;
    //}
    //public void BuildMessageBox(string caption, string info, string but1, string but2)
    //{
    //    BuildMessageBox(caption, info, but1);
    //    button2.gameObject.SetActive(true);
    //    _buttonText2.text = but2;
    //}

}
