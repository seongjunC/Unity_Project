using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class NextFloorSceneLoader : MonoBehaviour
{


    public string targetSpawnPointId;

    [SerializeField]
    private string sceneName;
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;

        SceneChangeManager sceneInstance = Manager.SceneChanger;
        if(sceneInstance != null){
            Debug.Log("" + sceneInstance.name);
        }
        sceneInstance.ChangeToScene(sceneName, targetSpawnPointId, other.gameObject); 
    }
}
