using EnumType;
using System;
using System.Collections.Generic;
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

    public struct PlayerStatData
    {
        public int damage;
        public int hp;
    }

    public struct SkillData
    {
        public string skillName;
        public float skillPower;
        public float coolTime;
    }

    public struct SkillVector
    {
        public Vector3 forward;
        public Vector3 right;
        public Vector3 up;
    }

    [Serializable]
    public struct BuffData
    {
        public StatType type;
        public float duration;
    }

    [Serializable]
    public struct DropData
    {
        public ItemData item;

        [Range(0, 100)] 
        public float chance;
    }
}
