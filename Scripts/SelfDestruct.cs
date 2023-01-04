using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip ("Time in Seconds, when particle clone is destroyed")] [SerializeField] float TimeToDed = 3f;
    void Start()
    {
        Destroy(gameObject, TimeToDed);
    }
}
