using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);       
    }
}
