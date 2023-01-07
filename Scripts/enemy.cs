using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] int ScorePerHit = 1;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int Hitpoints = 2;

    Scoreboard score;
    GameObject parentGameObject;
    void Start()
    {
       score = FindObjectOfType<Scoreboard>(); //findOBjectOfType very recource heavy

        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (Hitpoints <= 0){
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Hitpoints --;
        score.IncreaseScore(ScorePerHit);
    }
    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
