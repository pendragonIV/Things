using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    protected RectTransform rectTransform;
    [SerializeField]
    protected TMP_Text text;
    [SerializeField]
    protected bool isMouseOverUI;
    [SerializeField]
    protected bool isDialogActive = false;
    [SerializeField]
    protected bool isDialogCanShow = false;

    public virtual void ShowDialog(RectTransform rectTransform)
    {
        rectTransform.gameObject.SetActive(true);
        isDialogActive = true;
    }

    public virtual void HideDialog(RectTransform rectTransform)
    {
        rectTransform.gameObject.SetActive(false);
        isDialogActive = false;
    }

    public virtual void SetText(string textString)
    {
        text.text = textString;
    }

    public virtual void IsMouseOverUI()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            isMouseOverUI = true;
        }
        else
        {
            isMouseOverUI = false;
        }
    }


}
