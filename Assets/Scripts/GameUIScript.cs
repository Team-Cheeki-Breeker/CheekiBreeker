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
    public float finalCountDown;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float minutes = Mathf.FloorToInt(finalCountDown / 60);
        float seconds = Mathf.FloorToInt(finalCountDown % 60);
        finalCountDownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (!paused)
        {
            if(gameEnd())
            {
                Time.timeScale = 0f;
                LoseScreen.SetActive(true);
                SetPaused(true);
            }
        }           

        if (Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0f;
            PauseScreen.SetActive(true);
            SetPaused(true);
        }
    }

    Boolean gameEnd()
    {
        
        finalCountDown -= Time.deltaTime;
        if (finalCountDown <= 0) finalCountDown = 0;
        return finalCountDown <= 0 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>().IsDead();
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
}

