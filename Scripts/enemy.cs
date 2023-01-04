using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] int ScorePerHit = 1;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int Hitpoints = 2;

    Scoreboard score;
    void Start()
    {
       score = FindObjectOfType<Scoreboard>(); //findOBjectOfType very recource heavy

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
        vfx.transform.parent = parent;
        Hitpoints --;
        score.IncreaseScore(ScorePerHit);
    }
    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
