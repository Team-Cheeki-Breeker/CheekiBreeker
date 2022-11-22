using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferenceController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider difficultySlider;

    private void Start()
    {
        LoadValues();
    }
   
    public void SavePrefsButton()
    {
        float volumeValue = volumeSlider.value;
        float difficultyValue= difficultySlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        PlayerPrefs.SetFloat("DifficultyValue", difficultyValue);
        LoadValues();
    }
    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        float diffValue = difficultySlider.value;
        volumeSlider.value = volumeValue;
        difficultySlider.value = diffValue;
        AudioListener.volume = volumeValue;
    }
}
