using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InGameLevelLoader : MonoBehaviour
{

    public static InGameLevelLoader instance;

    public GameObject loadingScreen;
    public GameObject mainMenu;
    public GameObject text;
    //public Slider slider;
    public Image throbber;
    private float currentValue;
    private float targetValue;
    public Animator breathe;
    public AudioMixer mainMixer;
    public Image image;
    public Text textlmao;
    public Text headerText;

    public Sprite[] images;
    public string[] texts;
    // Multiplier for progress animation speed.
    [SerializeField]
    [Range(0, 1)]
    private float progressAnimationMultiplier = 1f;
    public void LoadLevel(int sceneIndex)
    {
        //StartCoroutine(FadeMixerGroup.StartFade(mainMixer, "volume", 2, 0));
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void LoadLevelWithoutAudio(int sceneIndex)
    {
        //StartCoroutine(FadeMixerGroup.StartFade(mainMixer, "volume", 2, 0));
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        breathe = GetComponent<Animator>();
    }

public static class FadeMixerGroup {
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }
}
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        mainMenu.SetActive(false);

        //image.sprite = images[SaveManager.instance.activeSave.level - 1];
        //textlmao.text = texts[SaveManager.instance.activeSave.level - 1];
        image.sprite = images[sceneIndex];
        textlmao.text = texts[sceneIndex];
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            targetValue = operation.progress / 0.9f;

            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            //slider.value = currentValue;
            throbber.fillAmount = currentValue;

            if (throbber.fillAmount == 1 && currentValue == 1)
            {
                text.SetActive(true);
                breathe.Play("TextBreathe");
                if (Input.GetKeyDown(KeyCode.Space))
                    operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
