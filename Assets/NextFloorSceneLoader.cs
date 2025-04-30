using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloorSceneLoader : MonoBehaviour
{
    static int thisFloor = 1;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("player"))
            return;

        SceneManager.LoadScene(thisFloor++);       
    }
}
