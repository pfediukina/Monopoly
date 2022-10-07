using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private GameManager gameManager;


    [Header("Components")]
    [SerializeField] private Button buttonRoll;
    [SerializeField] private Button buttonList;
    [SerializeField] private MessageBoxUI messageBox;
    [SerializeField] private ListBoxUI listBox;
    [SerializeField] private PlayerStatsUI playerUI;

    private DialogID _currentDialog;

    private void Start()
    {
        buttonRoll.onClick.AddListener(gameManager.OnPlayerRolled);
        buttonList.onClick.AddListener(OnPlayerList);
    }

    public void HideDialogs()
    {
        messageBox.SetMessageActive(false);
        listBox.SetListActive(false);
    }

    public void ShowPlayerDialog(UnitController player_id, DialogID dialog_id, string caption, string info, string button_1, string button_2)
    {
        buttonRoll.interactable = false;
        messageBox.SetMessageActive(true);
        _currentDialog = dialog_id;
        if (button_2 == "")
            messageBox.BuildMessageBox(caption, info, button_1);
        else
            messageBox.BuildMessageBox(caption, info, button_1, button_2);
    }

    public void OnPlayerPressedButton(bool responce)
    {
        buttonRoll.interactable = true;
        gameManager.OnDialogResponse(_currentDialog, responce);
        
    }

    public void UpdatePropertyList(UnitController player, Board board)
    {
        List<Tile> tiles = board.GetAllTiles().Where(p => p.GetTileInfo().Owner == player &&
                                                         p.GetTileInfo().Owner != null).ToList();

        playerUI.UpdatePlayerProperty(tiles);
    }

    public void UpdatePlayerMoney(int current_money, Color color)
    {
        playerUI.UpdatePlayerMoney(current_money);
        playerUI.SetTextColor(color);
    }

    public void UpdatePlayerMoney(int start_money, int change_val, int current_money)
    {
        playerUI.ChangePlayerMoneyTextWithValue(start_money, change_val, current_money);
    }

    public void OnPlayerList()
    {
        _currentDialog = DialogID.List;
        var player = gameManager.GetCurrentPlayer();
        var prop = gameManager.GetPlayerProperty(player);
        listBox.SetList(prop);
        if(player.GetPlayerInfo().Money > 0)
            listBox.ChangeDoubtAndMoney(0);
        else
            listBox.ChangeDoubtAndMoney(player.GetPlayerInfo().Money, 0);

        listBox.ChangeDoubtAndMoney(player.GetPlayerInfo().Money > 0 ? 1 : player.GetPlayerInfo().Money, 0);
        listBox.SetListActive(true);
    }

    public ListBoxUI GetList()
    {
        return listBox;
    }
}
