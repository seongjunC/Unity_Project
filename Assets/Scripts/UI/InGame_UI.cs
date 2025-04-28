using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_UI : MonoBehaviour
{
    private GameObject curUI;


    public void SwitchUI(GameObject ui)
    {
        if(ui.activeSelf)
        {
            ui.SetActive(false);
            curUI = null;
        }
        else
        {
            if(curUI != null)
            {
                curUI.SetActive(false);
            }

            ui.SetActive(true);
            curUI = ui;
        }
    }
}
