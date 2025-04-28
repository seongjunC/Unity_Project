using System;
using UnityEngine;

namespace StructType
{
    [Serializable]
    public struct PoolSize
    {
        public string name;
        public int size;
    }
    [Serializable]
    public struct itemMeshData
    {
        public Mesh mesh;
        public Material material;
    }
}
