using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_UI : MonoBehaviour
{
    [SerializeField] 
    private FadeScreen fadeScreen;
    [SerializeField]
    private string spawnId;
    [SerializeField]
    private string sceneName;
    private WaitForSeconds wft = new WaitForSeconds(1f);
    private WaitForSeconds wft2 = new WaitForSeconds(0.5f);

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }
    public void OutGame()
    {
        Application.Quit();
    }

    IEnumerator StartGameRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        yield return wft;

        Manager.SceneChanger.ChangeToScene(sceneName, spawnId);

        yield return wft2;

        fadeScreen.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync("TitleScene");

        //SceneManager.LoadSceneAsync("InGame1");
    }
}
