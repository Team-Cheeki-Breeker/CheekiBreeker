using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour
{

    private int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
        {
            nextScene = 0;
        }
        else
        {
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") return;
        SceneManager.LoadScene(nextScene);
    }

}
