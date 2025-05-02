using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    private List<string> loadedScenes = new List<string>();
    private WaitForSeconds wft;
    void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        wft = new WaitForSeconds(0.3f);
    }

    public void ChangeToScene(string targetSceneName, string spawnPointId, GameObject player){
        StartCoroutine(CheckScene(targetSceneName,spawnPointId, player));
    }

    IEnumerator CheckScene(string targetSceneName,string spawnPointId, GameObject player){
        if(!loadedScenes.Contains(targetSceneName)){
            yield return SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
            loadedScenes.Add(targetSceneName);
        }

        Scene scene = SceneManager.GetSceneByName(targetSceneName);

        SceneManager.SetActiveScene(scene);
        yield return wft;

        SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

        Debug.Log($"{spawnPoints.Length}, spLength");
        foreach(SpawnPoint spawnPoint in spawnPoints){
            Debug.Log(spawnPoint.name);
            Debug.Log(spawnPoint.spawnId);
            if(spawnPoint.spawnId == spawnPointId){
                player.transform.position = spawnPoint.transform.position;
                player.transform.rotation = spawnPoint.transform.rotation;
                yield break;
            }
            else{Debug.Log($"{spawnPoint.spawnId}, 아님");}
        }
    }
}
