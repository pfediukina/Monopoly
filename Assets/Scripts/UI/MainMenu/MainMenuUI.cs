using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private UnitController unit;

    [Header("Components")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        unit.SetPlayerColor();
        exitButton.onClick.AddListener(OnClickExit);
        startButton.onClick.AddListener(OnClickStart);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
