using UnityEngine;

public class EscapePanel : MonoBehaviour
{
    [SerializeField] private InGame_UI ui;


    public void Escape()
    {
        Application.Quit();
    }

    public void Out()
    {
        ui.SwitchUI(ui.escapePanel);
    }
}
