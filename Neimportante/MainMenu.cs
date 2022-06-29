using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
public class MainMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public AudioMixer sfxmixer;
    public Dropdown resolutionDropdown;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Dropdown graphics;
    public Toggle fullscreen;
    public Text text;


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        string path = Application.persistentDataPath + "/" + "save1" + ".save";
        //If there is a saved game, change the button's text from New Game to Continue
        //This is useless cuz I made another script that activates a button "Continue"
        if (File.Exists(path))
        {
            text.text = "Continue";
        }
        else
        {
            text.text = "New Game";
        }
        //Applies all saved slider values in the settings menu
        if (PlayerPrefs.HasKey("volume"))
        {
            audiomixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volume")) * 20);
            musicSlider.value = PlayerPrefs.GetFloat("volume");
        }
        if (PlayerPrefs.HasKey("SFXvolume"))
        {
            sfxmixer.SetFloat("SFXvolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXvolume")) * 20);
            SFXSlider.value = PlayerPrefs.GetFloat("SFXvolume");
        }
        if (PlayerPrefs.HasKey("qualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
            graphics.value = PlayerPrefs.GetInt("qualityLevel");
        }
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            if (PlayerPrefs.GetInt("isFullscreen") == 1)
            {
                fullscreen.isOn = true;
                Screen.fullScreen = true;
            }
            else
            {
                fullscreen.isOn = false;
                Screen.fullScreen = false;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
