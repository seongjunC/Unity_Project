using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PoolData")]
public class PoolData : ScriptableObject
{
    [Serializable]
    public struct PoolSize
    {
        public string name;
        public int size;
    }

    public PoolSize[] poolSize;
}
