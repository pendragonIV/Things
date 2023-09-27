using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : DialogSystem
{
    private void Update()
    {
        IsMouseOverUI();

        if (isDialogCanShow)
        {
            if (!isDialogActive)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShowDialog(rectTransform);
                    SetText("This is your garden");
                }
            }
            else if (!isMouseOverUI && isDialogActive)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    HideDialog(rectTransform);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDialogCanShow = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDialogCanShow = false;
    }

}
