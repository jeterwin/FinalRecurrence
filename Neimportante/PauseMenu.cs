using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PickUpObject ItemInstance;
    public static PauseMenu instance;
    public static bool IsGamePaused = false;
    public bool canPause = false, hasItem = false;
    public UnityEvent pressedESCEvent;

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
        //If ESC or the Options button (on joysticks) is pressed, check if the game is paused or not:
        //if it is : resume / if it isn't : pause
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
                    //If you are able to pause (you are not allowed to pause during cutscenes) and you are currently reading
                    //a note inside the note system, de-activate the note system, get the note off the screen and pause the game
                    if (hasItem == true)
                        ItemInstance.DropItem();
                    NoteSystem.instance.CloseNote(true);
                    NoteSystem.instance.Close(true);
                    if(NoteSystem.instance.usingNotesSystem)
                    NoteSystem.instance.usingNotesSystem = !NoteSystem.instance.usingNotesSystem;
                    pressedESCEvent.Invoke();
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
        //Let the audio listener catch sounds, make the cursor invisible and unlock it from the middle of the screen
        //de-activate the pause menu and the settings menu (in case the player is in the settings menu) let time run normal again
        //and let the player move freely
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
        //Well, all of the above but reversed basically, and in the main menu
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
        //Well, all of the above but reversed basically
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
        //Load a certain scene (after its given index) and don't allow the scene to be activated until it was loaded
        //also de-activate the main menu and activate the loading screen ui
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        //While the operation isn't done
        while (!operation.isDone)
        {
            //Divide it by 0.9 so it can return "1" as a value;
            targetValue = operation.progress / 0.9f;

            //Make the current loading value go from it's current one to the target one in a specified time and update
            //the slider with how much the level has loaded
            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            slider.value = currentValue;

            //Once the slider value reached 1 (the scene is fully loaded), display the "Press Space to continue" text
            //and play its animation (the breathing one)
            //If the player presses space, activate the next scene / level.
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
        //Yes
        Application.Quit();
    }
}
