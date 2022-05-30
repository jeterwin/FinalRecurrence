using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    public GameObject loadingScreen;
    public GameObject mainMenu;
    public GameObject text;
    public Image throbberImage;
    //public Slider slider;
    public Image image;
    public Text textlmao;
    public Text headerText;

    private float currentValue;
    private float targetValue;
    private float elapsedTime;

    public Animator breathe;
    public AudioMixer mainMixer;
    public CanvasGroup canvasGroup;
    
    public Sprite[] images;
    public string[] texts;
    public string[] headerTexts;
    // Multiplier for progress animation speed.
    [SerializeField]
    [Range(0, 1)]
    private float progressAnimationMultiplier = 1f;
    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData1();
        StartCoroutine(LoadAsynchronously(1));

    }
    public void LoadedLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        breathe = GetComponent<Animator>();
        if(!canvasGroup) return;
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
         IEnumerator DoFadeOut()
     {
         while(canvasGroup.alpha > 0)
         {
             elapsedTime += Time.deltaTime;
             canvasGroup.alpha = Mathf.Clamp01(1.0f - (elapsedTime / 2));
             yield return null;
         }
 
         yield return null;
     }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        mainMenu.SetActive(false);

        image.sprite = images[SaveManager.instance.activeSave.chapter];
        textlmao.text = texts[SaveManager.instance.activeSave.chapter];
        headerText.text = headerTexts[SaveManager.instance.activeSave.chapter];
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            targetValue = operation.progress / 0.9f;

            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            //slider.value = currentValue;
            throbberImage.fillAmount = currentValue;

            if (throbberImage.fillAmount == 1 && currentValue == 1)
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
