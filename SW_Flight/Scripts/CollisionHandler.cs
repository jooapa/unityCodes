using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Need to be for Scene control to work

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float InvokeTime = 1f;
    [SerializeField] ParticleSystem crashVFX;

    PlayerController DeLaser;

    void OnTriggerEnter(Collider other){
        StartCrashSequence();
    }
    void StartCrashSequence(){
        DeLaser = FindObjectOfType<PlayerController>();
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", InvokeTime);
        DeLaser.SetLasersActive(false);


    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
