using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneUI : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        StartCoroutine(LoadAsyncRoutine());
    }

    IEnumerator LoadAsyncRoutine()
    {
        yield return StartCoroutine(Manager.Data.SetupGameDataWithProgress((p) =>
        {
            loadingSlider.value = Mathf.Lerp(0f, 0.7f, p);
        }));

        AsyncOperation op = SceneManager.LoadSceneAsync("HSDTestScene");
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            loadingSlider.value = Mathf.Lerp(0.7f, 1f, op.progress / 0.9f);
            yield return null;
        }

        loadingSlider.value = 1f;

        Manager.SceneChanger.FadeIn();
        yield return new WaitForSeconds(1f);

        SceneManager.sceneLoaded += OnSceneLoaded;
        op.allowSceneActivation = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HSDTestScene")
        {
            Manager.Data.PostSceneInit();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Manager.SceneChanger.FadeOut();
        }
    }
}
