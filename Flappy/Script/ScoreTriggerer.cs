using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerer : MonoBehaviour
{
    //Reference to another script in the project
    public Manager Manager;


    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 3){
        Manager.addScore(1);
        }
    }
}
