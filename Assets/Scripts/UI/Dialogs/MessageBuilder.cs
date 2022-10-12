using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBuilder: MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public TextMeshProUGUI titleText;
    [SerializeField] public TextMeshProUGUI descText;
    [SerializeField] public TextMeshProUGUI button1Text;
    [SerializeField] public TextMeshProUGUI button2Text;
    [SerializeField] public Button button1;
    [SerializeField] public Button button2;
}
