using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject PipePrefab;
    [SerializeField] float spawnRate = 2f;
    [SerializeField] float heightOffset = 2f;
    private float timer = 0;

    void Update()
    {
        if(timer < spawnRate){

        timer += Time.deltaTime;
        }
        else{
        spawnPipe();
        timer = 0;
        }
    }
    void spawnPipe(){
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(PipePrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation); //Spawns on top to the Pipe Spawner (transform.position || rotation)
    }
}
