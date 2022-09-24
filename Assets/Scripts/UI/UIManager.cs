using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DialogBuilder dialogBuilder;

    [Header("Components")]
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button buttonRoll;


    private DialogID currentDialog;

    private void Start()
    {
        button1.onClick.AddListener(delegate
        {
            OnPlayerPressedButton(button1);
        });
        button2.onClick.AddListener(delegate
        {
            OnPlayerPressedButton(button2);
        });
        buttonRoll.onClick.AddListener(OnPlayerRoll);

    }

    public void ShowPlayerDialog(UnitController player_id, DialogID dialog_id, string caption, string info, string button_1, string button_2)
    {
        currentDialog = dialog_id;
        if(dialog_id != DialogID.List)
        {
            button2.gameObject.SetActive(true);
            dialogBuilder.BuildMessageDialog(caption, info, button_1, button_2);
            buttonRoll.interactable = false;
            if(button_2 == "")
            {
                button2.gameObject.SetActive(false);
            }
        }
    }

    public void HidePlayerDialog()
    {
        dialogBuilder.HideMessageDialog();
    }

    private void OnPlayerPressedButton(Button button)
    {
        bool responce = button == button1 ? true : false;
        gameManager.OnDialogResponse(currentDialog, responce);
        buttonRoll.interactable = true;
    }

    private void OnPlayerRoll()
    {
        gameManager.OnPlayerRolled();
    }
}
