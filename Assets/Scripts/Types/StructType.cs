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
    public struct ItemMeshData
    {
        public Mesh mesh;
        public Material material;
    }

    [Serializable]
    public struct SkillData
    {
        public string skillName;
        public int skillPower;
        public float coolTime;
    }

    public struct SkillVector
    {
        public Vector3 forward;
        public Vector3 right;
        public Vector3 up;
    }
}
