using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int playerScore;
    public Text ScoreText;
    public GameObject GameOverScreen;
    public GameObject[] pipes;
    public GameObject pipeSpawner;

    [ContextMenu("INC Score")]
    public void addScore(int x){
        playerScore += x;
        ScoreText.text = playerScore.ToString();
    }

    public void gameOver(){
        GameOverScreen.SetActive(true);
        foreach (GameObject r in pipes){
            r.gameObject.GetComponent<PipeMoveScript>().pipeSpeed = 0;
        }
        pipeSpawner.GetComponent<pipeSpawnScript>().enabled = false;
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void Update(){
        pipes = GameObject.FindGameObjectsWithTag("pipe");
    }
    
}
