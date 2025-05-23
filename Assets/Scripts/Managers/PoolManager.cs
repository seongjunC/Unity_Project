using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, IObjectPool<GameObject>> poolDic;
    private Dictionary<string, Transform> poolParent;
    private Dictionary<string, float> lastUsedTime;
    private ObjectPool<GameObject> popUpPool;

    private Transform parent;
    private Transform popUpParent;

    Coroutine poolCleanupRoutine;

    const float poolCleanupTime = 60;
    const float poolCleanupDelay = 30;

    public PoolData poolData;
    public Transform UI;

    public void Init()
    {
        poolDic = new Dictionary<string, IObjectPool<GameObject>>();
        poolParent = new Dictionary<string, Transform>();
        lastUsedTime = new Dictionary<string, float>();

        parent = new GameObject("Pool Parent").transform;
        poolData = Manager.Data.poolData;

        GameObject canvasObj = Manager.Game.canvas;
        UI = canvasObj.transform;

        popUpParent = new GameObject("PopUpParent").transform;
        popUpParent.parent = UI.transform;
    }

    private void CreatePopUpTextPool(GameObject popUp)
    {
        popUpPool = new ObjectPool<GameObject>(
            () =>
            {
                var obj = Instantiate(popUp, popUpParent);
                obj.name = popUp.name;
                return obj;
            },
            obj =>
            {
                obj.SetActive(true);
                obj.transform.SetParent(null);
            },
            obj =>
            {
                obj.SetActive(false);
                obj.transform.SetParent(popUpParent);
            },
            obj =>
            {
                Destroy(obj);
            },
            maxSize: 15
        );
    }

    public GameObject GetPopUp (GameObject popUp, Vector3 position)
    {
        if (popUpPool == null)
            CreatePopUpTextPool(popUp);

        GameObject obj = popUpPool.Get();
        obj.transform.position = position;

        return obj;
    }

    public void ReleasePopUp(GameObject popUp)
    {
        if (popUpPool != null)
            popUpPool.Release(popUp);
    }

    private void Start()
    {
        poolCleanupRoutine ??= StartCoroutine(PoolCleanupRoutine());
    }

    IEnumerator PoolCleanupRoutine()
    {
        YieldInstruction delay = new WaitForSeconds(poolCleanupDelay);

        while (true)
        {
            yield return delay;

            float now = Time.time;
            List<string> removePoolKeys = new List<string>();

            foreach (var kvp in poolDic)
            {
                string key = kvp.Key;

                if (lastUsedTime.TryGetValue(key, out float lastTime))
                {
                    if (now - lastTime > poolCleanupTime)
                    {
                        removePoolKeys.Add(key);
                    }
                }
            }

            foreach (var value in removePoolKeys)
            {
                poolDic.Remove(value);

                if(poolParent[value].gameObject != null)
                    Manager.Resources.Destroy(poolParent[value].gameObject);

                poolParent.Remove(value);
                lastUsedTime.Remove(value);
            }
        }
    }

    public void StopPoolCleanupRoutine()
    {
        StopCoroutine(poolCleanupRoutine);
        poolCleanupRoutine = null;
    }

    private IObjectPool<GameObject> GetOrCreatePool(string name, GameObject go, int maxSize)
    {
        if(poolDic.ContainsKey(name))
            return poolDic[name];

        Transform root = new GameObject($"{name} Pool").transform;
        root.transform.SetParent(parent, false);
        poolParent.Add(name, root);
            
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>
        (
            createFunc: () =>
            {
                GameObject obj = Instantiate(go);
                obj.name = name;
                obj.transform.SetParent(root, false);
                lastUsedTime[name] = Time.time;
                return obj;
            },
            actionOnGet: (GameObject obj) =>
            {
                obj.SetActive(true);
                obj.transform.SetParent(null, false); 
                lastUsedTime[name] = Time.time;
            },
            actionOnRelease: (GameObject obj) =>
            {
                obj.SetActive(false);
                obj.transform.SetParent(root, false);
            },
            actionOnDestroy: (GameObject obj) =>
            {
                Destroy(obj);
            },
            maxSize: maxSize
        );

        poolDic.Add(name, pool);
        return pool;
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        GameObject go = original as GameObject;
        string name = go.name;

        int maxSize = 5;

        for (int i = 0; i < poolData.poolSize.Length; i++)
        {
            if (poolData.poolSize[i].name == name)
            {
                maxSize = poolData.poolSize[i].size;
                break;
            }
        }

        var pool = GetOrCreatePool(name, go, maxSize);

        GameObject obj = pool.Get();

        if(parent != null)
            obj.transform.SetParent(parent, false);

        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;

        return obj as T;
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Get(original, position, rotation, null);
    }

    public void Release<T>(T original) where T : Object
    {
        GameObject obj = original as GameObject;
        string name = obj.name;

        if (!ContainsKey(name) && !obj.activeSelf)
            return;

        poolDic[name].Release(obj);
    }

    public void Release<T>(T original, float delay) where T : Object
    {
        GameObject obj = original as GameObject;
        string name = obj.name;

        if (!ContainsKey(name) && !obj.activeSelf)
            return;

        StartCoroutine(DelayRelease(original, delay));
    }

    IEnumerator DelayRelease<T>(T original, float delay) where T : Object
    {
        yield return new WaitForSeconds(delay);

        GameObject obj = original as GameObject;
        string name = obj.name;

        if (ContainsKey(name) && obj.activeSelf)
            poolDic[name].Release(obj);
    }

    public bool ContainsKey(string key)
    {
        return poolDic.ContainsKey(key);
    }
}
