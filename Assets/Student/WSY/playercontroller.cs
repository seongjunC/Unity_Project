using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{

    [SerializeField] public float rotatespeed;
    [SerializeField] public float speed;
    [SerializeField] public int hp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        float input = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * input * speed * Time.deltaTime);

    }

    public void Rotate()
    {
        float input = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * input * rotatespeed * Time.deltaTime);

    }

}
