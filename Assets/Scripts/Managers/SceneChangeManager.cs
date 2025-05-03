using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    private List<string> loadedScenes = new List<string>();
    private WaitForSeconds wft;
    private GameObject fade_UI;
    private Animator fadeAnim;

    void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        wft = new WaitForSeconds(0.3f);

        fade_UI = Manager.Resources.Instantiate<GameObject>("Fade", transform.position);
        fade_UI.transform.SetParent(transform, false);

        fadeAnim = fade_UI.GetComponentInChildren<Animator>();
    }

    public void FadeIn() => fadeAnim.SetTrigger("In");
    public void FadeOut() => fadeAnim.SetTrigger("Out");

    public void ChangeToScene(string targetSceneName, string spawnPointId){
        StartCoroutine(CheckScene(targetSceneName,spawnPointId));
    }

    public void ChangeToScene(string targetSceneName, string spawnPointId, GameObject player){
        StartCoroutine(CheckScene(targetSceneName,spawnPointId, player));
    }

    IEnumerator CheckScene(string targetSceneName,string spawnPointId, GameObject player = null){
        if(!loadedScenes.Contains(targetSceneName)){
            yield return SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
            loadedScenes.Add(targetSceneName);
        }

        Scene scene = SceneManager.GetSceneByName(targetSceneName);

        SceneManager.SetActiveScene(scene);
        yield return wft;

        SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
        
        if(player ==null){
            Debug.Log($"{targetSceneName}, player null");
            yield break;
        }
        foreach(SpawnPoint spawnPoint in spawnPoints){
            if(spawnPoint.spawnId == spawnPointId){
                player.transform.position = spawnPoint.transform.position;
                player.transform.rotation = spawnPoint.transform.rotation;
                yield break;
            }
            else{Debug.Log($"{spawnPoint.spawnId}, 아님");}
        }
    }
}
