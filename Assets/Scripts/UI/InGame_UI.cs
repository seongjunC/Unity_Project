using UnityEngine;

public class InGame_UI : MonoBehaviour
{
    private GameObject curUI;
    [SerializeField] private GameObject inventoryPenal;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject optionPanel;
    public GameObject escapePanel;


    private void Start()
    {
        AllUIClose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchUI(inventoryPenal);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchUI(optionPanel);

        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchUI(escapePanel);
    }

    private void Out()
    {
        Application.Quit();
    }

    public void SwitchUI(GameObject ui)
    {
        if(ui.activeSelf)
        {
            ui.SetActive(false);
            curUI = null;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if(curUI != null)
            {
                curUI.SetActive(false);
            }

            ui.SetActive(true);
            curUI = ui;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void AllUIClose()
    {
        inGamePanel.SetActive(true);
        inventoryPenal.SetActive(false);
    }
}
