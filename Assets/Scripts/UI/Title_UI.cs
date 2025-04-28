using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_UI : MonoBehaviour
{
    [SerializeField] private FadeScreen fadeScreen;

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

        yield return new WaitForSeconds(2);
        //SceneManager.LoadSceneAsync("InGame1");
    }
}
