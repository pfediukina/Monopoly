using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sRollButton : MonoBehaviour
{
    [SerializeField]
    public Button rollButt;
    public TextMeshPro textMesh;
    public UnitController player;


    private int _num1 = 0;
    private int _num2 = 0;

    private void Start()
    {
        Button btn = rollButt.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {  
        _num1 = Random.Range(1, 6);
        _num2 = Random.Range(1, 6);
        string text = $"{_num1.ToString()} {_num2.ToString()}";
        textMesh.text = text;
        player.Move(_num1 + _num2);
    }
}
