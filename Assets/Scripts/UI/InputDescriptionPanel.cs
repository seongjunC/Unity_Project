using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDescriptionPanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Destroy(gameObject);
    }
}
