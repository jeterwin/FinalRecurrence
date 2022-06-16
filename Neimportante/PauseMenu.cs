using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static PauseMenu instance;
    public static bool IsGamePaused = false;
    public bool canPause = false;

    public GameObject loadingScreen;
    public GameObject mainMenu;
    public GameObject text;
    public GameObject settingsMenu;

    public Slider slider;
    private float currentValue;
    private float targetValue;
    public Animator breathe;
    [SerializeField]
    [Range(0, 1)]
    private float progressAnimationMultiplier = 0.25f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                if (canPause == true)
                {
                    NoteSystem.instance.CloseNote(true);
                    NoteSystem.instance.Close(true);
                    if(NoteSystem.instance.usingNotesSystem)
                    NoteSystem.instance.usingNotesSystem = !NoteSystem.instance.usingNotesSystem;
                    Pause();
                }
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }
    public void Resume()
    {
        AudioListener.volume = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        Fps_Script.instance.canMove = true;
    }
    public void ResumeInMainMenu()
    {
        //AudioListener.volume = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        Fps_Script.instance.canMove = false;
    }
    public void Pause()
    {
        AudioListener.volume = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        Fps_Script.instance.canMove = false;
    }
    public void LoadLevel(int sceneIndex)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            targetValue = operation.progress / 0.9f;

            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            slider.value = currentValue;

            if (slider.value == 1)
            {
                text.SetActive(true);
                breathe.Play("TextBreathe");
                if (Input.GetKeyDown(KeyCode.Space))
                    operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
