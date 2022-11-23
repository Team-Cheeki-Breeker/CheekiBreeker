using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour
{
    public GameObject LoseScreen; 
    public GameObject PauseScreen;
    public TextMeshProUGUI finalCountDownText;
    public TextMeshProUGUI GoalText;
    public float finalCountDown;
    private bool paused = false;
    private bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
        paused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = GameObject.FindGameObjectWithTag("Player").transform.position - GameObject.FindGameObjectWithTag("Goal").transform.position;
        GoalText.text = "Objective:\nReach the Lada (" + ((int) Math.Round( difference.magnitude)).ToString() +"m)";

        float minutes = Mathf.FloorToInt(finalCountDown / 60);
        float seconds = Mathf.FloorToInt(finalCountDown % 60);
        finalCountDownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (!paused)
        {
            if(gameEnd() )
            {
                gameEnded = true;
                LoseScreen.SetActive(true);
                SetPaused(true);
            }
        }           

        if (Input.GetKeyDown(KeyCode.Escape)){
            PauseScreen.SetActive(true);
            SetPaused(true);
        }
    }

    Boolean gameEnd()
    {       
        finalCountDown -= Time.deltaTime;
        if (finalCountDown <= 0) finalCountDown = 0;
<<<<<<< HEAD
        return finalCountDown <= 0 || gameEnded;
=======
        return finalCountDown <= 0 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>().IsDead();
>>>>>>> a8d345b0d7d8fda384dffc156492d64763f8c959
    }

    public void SetPaused(bool b)
    {
        paused = b;
        if (paused) Time.timeScale = 0f;
        if (!paused) Time.timeScale = 1f;

    }

    public void QuitButton()
    {   
        SetPaused(false);
        SceneManager.LoadScene(0); 
    }

    public void RestartButton()
    {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        gameEnded = true;
    }
}

