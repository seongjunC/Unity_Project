using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillbordObejct : MonoBehaviour
{
    protected virtual void Update()
    {
        Billboard();
    }

    private void Billboard()
    {
        Vector3 dir = Camera.main.transform.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
