using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearPanel : MonoBehaviour
{
    public void ChangeTitleScene() => SceneManager.LoadSceneAsync("TitleScene");
}
