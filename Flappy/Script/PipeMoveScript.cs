using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float pipeSpeed = 5;
    [SerializeField] float deadZone = -55;
    public Manager Manager;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
    }


    void Update()
    {
        transform.position = transform.position + (Vector3.left * pipeSpeed) * Time.deltaTime;


        if(transform.position.x < deadZone){
            Destroy(gameObject);
        }
    }


}
