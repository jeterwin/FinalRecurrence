using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int batteries;
    public SaveData activeSave;
    public Text currentObjectiveText;
    public Vector3 respawnPoint;

    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode crouch { get; set; }
    public KeyCode shift { get; set; }
    private void Awake()
    {
        instance = this;

        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey", "LeftControl"));
        shift = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shiftKey", "LeftShift"));
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Fps_Script.instance != null)
        respawnPoint = Fps_Script.instance.transform.position;

        if (SaveManager.instance.hasLoaded && SceneManager.GetActiveScene().buildIndex != 0)
        {
            activeSave = SaveManager.instance.activeSave;
            currentObjectiveText.text = SaveManager.instance.activeSave.currentObjective;
            Fps_Script.instance.gameObject.SetActive(false);
            respawnPoint = SaveManager.instance.activeSave.respawnPosition;
            Fps_Script.instance.transform.position = respawnPoint;
            Flashlight_PRO.instance.batteries = SaveManager.instance.activeSave.batteries;
            if(HealthSystem.instance != null)
            {
                HealthSystem.instance.DrugsAmount = SaveManager.instance.activeSave.medicines;
                HealthSystem.instance.sanityValue = SaveManager.instance.activeSave.sanitySystemSaved;
            }

            //Sa se salveze sanityu
            batteries = SaveManager.instance.activeSave.batteries;
        }
        else
        {
            if (Fps_Script.instance != null)
                SaveManager.instance.activeSave.batteries = batteries;
        }
        if (Fps_Script.instance != null)
            Fps_Script.instance.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        Fps_Script.instance.gameObject.SetActive(false);

        SaveManager.instance.activeSave.batteries = batteries;

        //yield return new WaitForSeconds(.5f);

        Fps_Script.instance.transform.position = respawnPoint;

        Fps_Script.instance.gameObject.SetActive(true);
        yield return null;
    }
}
