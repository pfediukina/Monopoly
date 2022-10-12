using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseDialog : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected CanvasGroup canvas;

    public abstract void OnDialogResponce(bool response);
    public abstract void SetDialogInfo(DialogInfo info);
    public abstract void Init();


    public void HidePlayerDialog()
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
    }

    public virtual void ShowPlayerDialog()
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
    }
}
