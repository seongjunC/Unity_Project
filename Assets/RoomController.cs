using System.Collections;
using System.Collections.Generic;
using DunGen;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    
    private List<GameObject> monsters;
    private List<doorRotate> Doors = new List<doorRotate>();
    private bool IsLock = true;

    void Start(){
        Doors.AddRange(GetComponentsInChildren<doorRotate>());
    }
    
    void Update()
    {
        // 잠김 상태이고 살아있는 몬스터가 없다면 열림 상태 전환
        if (IsLock && !IsMonsterAlive()){
            IsLock = false;
            foreach (doorRotate d in Doors){
                d.UnLockDoor();
            }
        }
        // 열림 상태이고 살아있는 몬스터가 있다면 닫힘 상태로 전환
        if (!IsLock && IsMonsterAlive()){
            IsLock = true;
            foreach (doorRotate d in Doors){
                d.LockDoor();
            }
        }
    }

    // 활성화된 몬스터를 찾을 수 있다면 true, 없다면 false를 반환
    private bool IsMonsterAlive(){
        Monster monster = FindAnyObjectByType<Monster>();
        return monster != null;
    }
}
