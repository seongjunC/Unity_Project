using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertest : MonoBehaviour
{
    float speed;   
    // Start is called before the first frame update
    void Start()
    {
        speed = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)){
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.A)){
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.S)){
            gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D)){
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime); 
        }
        
    }
}
