using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    private Dictionary<string, Object> resources = new Dictionary<string, Object>();    
    private Dictionary<string, Object> fx = new Dictionary<string, Object>();

    public T Load<T>(string path) where T : Object
    {
        if (resources.ContainsKey(path))
            return resources[path] as T;
        
        T resource = Resources.Load<T>(path);

        if(resource != null)
            resources.Add(path, resource);

        return resource;
    }
    public void Unload(string path)
    {
        if (resources.ContainsKey(path))
        {
            Resources.UnloadAsset(resources[path]);
            resources.Remove(path);
        }
    }
    public void UnloadAll()
    {
        foreach (var res in resources.Values)
            Resources.UnloadAsset(res);
        resources.Clear();
    }

    public void ClearResourcesCached() => resources.Clear();

    public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent, bool isPool = false) where T : Object
    {
        GameObject obj = original as GameObject;
        
        if (isPool)
            return Manager.Pool.Get(obj, position, rotation, parent) as T;
        else
            return GameObject.Instantiate(obj, position, rotation, parent) as T;
    }

    public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, bool isPool = false) where T : Object
    {
        return Instantiate (original, position, rotation, null, isPool);
    }

    public T Instantiate<T>(T original, Vector3 position, bool isPool = false) where T : Object
    {
        return Instantiate(original, position, Quaternion.identity, null, isPool);
    }
    
    public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent, bool isPool = false) where T : Object
    {
        T obj = Load<T>(path);
        return Instantiate(obj, position, rotation, parent, isPool);
    }

    public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, bool isPool = false) where T : Object
    {
        return Instantiate<T>(path, position, rotation, null, isPool);
    }

    public T Instantiate<T>(string path, Vector3 postion, bool isPool = false) where T : Object
    {
        return Instantiate<T>(path, postion, Quaternion.identity, null, isPool);
    }

    public void Destroy(GameObject obj)
    {
        if(Manager.Pool.ContainsKey(obj.name))
            Manager.Pool.Release(obj);
        else
            Object.Destroy(obj);
    }

    public void Destroy(GameObject obj, float delay)
    {
        if (Manager.Pool.ContainsKey(obj.name))
            Manager.Pool.Release(obj, delay);
        else
            Object.Destroy(obj, delay);
    }
}
