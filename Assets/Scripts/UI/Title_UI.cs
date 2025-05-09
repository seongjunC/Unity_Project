using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_UI : MonoBehaviour
{
    [SerializeField] private string spawnId;
    [SerializeField] private string sceneName;

    private WaitForSeconds wft = new WaitForSeconds(1f);
    private WaitForSeconds wft2 = new WaitForSeconds(.5f);

    private Coroutine gameStartRoutine;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        if (gameStartRoutine != null) return;

        gameStartRoutine = StartCoroutine(StartGameRoutine());
    }
    public void OutGame()
    {
        Application.Quit();
    }

    IEnumerator StartGameRoutine()
    {
        Manager.SceneChanger.FadeIn();
        yield return wft;

        Manager.SceneChanger.ChangeToScene(sceneName, spawnId);
        Manager.SceneChanger.FadeOut();
        yield return wft2;

        SceneManager.UnloadSceneAsync("TitleScene");
        gameStartRoutine = null;
    }
}
