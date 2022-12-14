using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int sceneNumber;

    public void PlayGame()
    {
        if(sceneNumber != 0)
            SceneManager.LoadScene(sceneNumber);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveMap(int map)
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex + map;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
