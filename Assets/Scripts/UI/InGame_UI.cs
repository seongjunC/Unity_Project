using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class InGame_UI : MonoBehaviour
{
    private GameObject curUI;
    [SerializeField] private GameObject inventoryPenal;
    [SerializeField] private GameObject inGamePanel;

    private void Start()
    {
        AllUIClose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchUI(inventoryPenal);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchUI(inGamePanel);
        
    }

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

    private void AllUIClose()
    {
        inGamePanel.SetActive(true);
        inventoryPenal.SetActive(false);
    }
}
