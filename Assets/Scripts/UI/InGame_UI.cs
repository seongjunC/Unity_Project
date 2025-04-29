using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class InGame_UI : MonoBehaviour
{
    private GameObject curUI;
    [SerializeField] private ItemToolTip toolTip;

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

    public void OpenToolTip(ItemData data)
    {
        toolTip.gameObject.SetActive(true);
        toolTip.SetupToolTip(data);
    }
    public void CloseToolTip() => toolTip.gameObject.SetActive(false);
}
