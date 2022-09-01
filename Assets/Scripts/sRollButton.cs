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

    private void Start()
    {
        Button btn = rollButt.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        string text = $"{Random.Range(1, 7).ToString()} {Random.Range(1, 6).ToString()}";
        textMesh.text = text;
    }

}
