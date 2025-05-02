using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager
{
    public static GameManager Game => GameManager.GetInstance();
    public static PoolManager Pool => PoolManager.GetInstance();
    public static ResourcesManager Resources => ResourcesManager.GetInstance();
    public static AudioManager Audio => AudioManager.GetInstance();
    public static DataManager Data => DataManager.GetInstance();
    public static SceneChangeManager SceneChanger => SceneChangeManager.GetInstance();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        GameManager.CreateManager();
        PoolManager.CreateManager();
        ResourcesManager.CreateManager();
        AudioManager.CreateManager();
        DataManager.CreateManager();
        SceneChangeManager.CreateManager();
    }
}
