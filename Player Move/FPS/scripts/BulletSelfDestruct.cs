using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSelfDestruct : MonoBehaviour
{
    [SerializeField] float TimetoKill = 3.5f;
    void Start()
    {
        Destroy(gameObject, TimetoKill);
    }

}
