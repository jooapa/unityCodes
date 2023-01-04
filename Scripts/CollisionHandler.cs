using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Need to be for Scene control to work

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float InvokeTime = 1f;
    [SerializeField] ParticleSystem crashVFX;
    void OnTriggerEnter(Collider other){
        StartCrashSequence();
    }
    void StartCrashSequence(){
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", InvokeTime);
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
