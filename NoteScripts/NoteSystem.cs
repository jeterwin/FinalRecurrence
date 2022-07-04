using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIElements
{
    [SerializeField] TextMeshProUGUI textObj;
    public TextMeshProUGUI TextObj { get { return textObj; } }

    [SerializeField] TextMeshProUGUI subscript;
    public TextMeshProUGUI Subscript { get { return subscript; } }

    [SerializeField] CanvasGroup subscriptGroup;
    public CanvasGroup SubscriptGroup { get { return subscriptGroup; } }

    [SerializeField] Image page;
    public Image Page { get { return page; } }

    [SerializeField] Image lines;
    public Image Lines { get { return lines; } }

    [SerializeField] CanvasGroup noteCanvasGroup;
    public CanvasGroup NoteCanvasGroup { get { return noteCanvasGroup; } }

    [SerializeField] CanvasGroup listCanvasGroup;
    public CanvasGroup ListCanvasGroup { get { return listCanvasGroup; } }

    [SerializeField] CanvasGroup readButton;
    public CanvasGroup ReadButton { get { return readButton; } }

    [SerializeField] CanvasGroup nextButton;
    public CanvasGroup NextButton { get { return nextButton; } }

    [SerializeField] CanvasGroup previousButton;
    public CanvasGroup PreviousButton { get { return previousButton; } }

    [SerializeField] NoteData noteDataPrefab;
    public NoteData NoteDataPrefab { get { return noteDataPrefab; } }
    [SerializeField] RectTransform listRect;
    public RectTransform ListRect { get { return listRect; } }
}
public class NoteSystem : MonoBehaviour
{
    #region Data and Actions

    [SerializeField] UIElements UI = new UIElements();

    [SerializeField] Color color1 = Color.gray;
    [SerializeField] Color color2 = Color.grey;

    private static Dictionary<String, Note> Notes = new Dictionary<string, Note>();
    private List<NoteData> noteDatas = new List<NoteData>();
    private static Action<Note> A_Display = delegate { };
    public static NoteSystem instance;

    #endregion

    #region Audio

    [SerializeField] private AudioSource[] sources = null;
    [Space]
    [SerializeField] private AudioClip[] openNoteSFX = null;
    [SerializeField] private AudioClip[] closeNoteSFX = null;
    [Space]
    [SerializeField] private AudioClip[] turnPageSFXs = null;
    [Space]
    [SerializeField] AudioSource openTabSound;
    [SerializeField] AudioClip[] openTabSounds;
    [Space]
    [SerializeField] AudioSource closeTabSound;
    [SerializeField] AudioClip[] closeTabSounds;
    #endregion

    #region Properties and Private

    public Note activeNote = null;
    private Page activePage
    {
        get
        {
            return activeNote.Pages[currentPage];
        }
    }
    private int currentPage = 0;
    private bool readSubscript = false;
    private Sprite defaultPageTexture = null;
    public bool usingNotesSystem;
    public Animator animator;
    #endregion

    #region Unity's default methods
    private void OnEnable()
    {
        A_Display += DisplayNote;
    }
    private void OnDisable()
    {
        A_Display -= DisplayNote;
    }
    private void Start()
    {
        Close(false);

        defaultPageTexture = UI.Page.sprite;
    }
    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton5)) && SaveManager.instance.activeSave.canOpenInventory && Fps_Script.instance.canMove == true)
        {
            usingNotesSystem = !usingNotesSystem;
            switch(usingNotesSystem)
            {
                case true:
                    Open();
                    openTabSound.clip = openTabSounds[UnityEngine.Random.Range(0, openTabSounds.Length)];
                    openTabSound.Play();
                    if(SaveManager.instance.activeSave.hasReachedSanity == true)
                        animator.Play("SanityCanvasFadeIn");
                    break;
                case false:
                    Close(activeNote != null);
                    closeTabSound.clip = closeTabSounds[UnityEngine.Random.Range(0, closeTabSounds.Length)];
                    closeTabSound.Play();
                    if (SaveManager.instance.activeSave.hasReachedSanity == true)
                        animator.Play("SanityCanvasFadeOut");
                    break;
            }
        }
    }

    #endregion

    #region Methods
    public void Awake()
    {
        instance = this;
    }
    public void Open()
    {
        SwitchGameControls(false);

        UpdateList();
        UpdateCanvasGroup(true, UI.ListCanvasGroup);
    }
    public void Close(bool playSFX)
    {
        SwitchGameControls(true);

        CloseNote(playSFX);
        UpdateCanvasGroup(false, UI.ListCanvasGroup);
    }

    private void SwitchGameControls(bool state)
    {
        switch(state)
        {
            case true:
                Fps_Script.instance.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                break;
            case false:
                Fps_Script.instance.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f;
                //Put the scale to 0 in case smth messes up
                break;
        }
    }

    private void DisplayNote(Note note)
    {

        if(note == null) { return; }

        SwitchGameControls(false);

        PlaySound(openNoteSFX[UnityEngine.Random.Range(0, openNoteSFX.Length)]);

        UpdateCanvasGroup(true, UI.NoteCanvasGroup);
        activeNote = note;

        DisplayPage(0);
    }
    private void DisplayPage(int page)
    {
        UI.ReadButton.interactable = activeNote.Pages[page].Type == PageType.Texture;

        if(activeNote.Pages[page].Type != PageType.Texture)
        { readSubscript = false; } else { if(readSubscript) { UpdateSubscript(); } }

        sources[1].Stop();
        if(activeNote.Pages[page].Narration != null)
        {
            if(!activeNote.Pages[page].NarrationPlayed)
            {
                sources[1].clip = activeNote.Pages[page].Narration;
                sources[1].Play();
                if(activeNote.Pages[page].Narration_PlayOnce)
                {
                    activeNote.Pages[page].NarrationPlayed = true;
                }
            }
        }

        switch(activeNote.Pages[page].Type)
        {
            case PageType.Text:
                UI.Page.sprite = defaultPageTexture;
                UI.TextObj.text = activeNote.Pages[page].Text;
                break;
            case PageType.Texture:
                UI.Page.sprite = activeNote.Pages[page].Texture;
                UI.TextObj.text = string.Empty;
                break;
        }
        UpdateUI();
    }

    public static void Display(Note note)
    {
        A_Display(note);
    }
    public static void Display(string key)
    {
        var note = GetNote(key);
        A_Display(note);
    }
    private void UpdateUI()
    {
        UI.PreviousButton.interactable = !(currentPage == 0);
        UI.NextButton.interactable = !(currentPage == activeNote.Pages.Length - 1);
        var useSubscript = activePage.Type == PageType.Texture && activePage.UseSubscript;
        UI.ReadButton.alpha = useSubscript ? (readSubscript ? 0.5f : 1f) : 0f;
        UpdateCanvasGroup(readSubscript, UI.SubscriptGroup);

        UI.Lines.enabled = activePage.DisplayLines;
    }
    private void UpdateList()
    {
        ClearList();

        var index = 0;
        var height = 0.0f;
        foreach(var note in Notes)
        {
            var color = index % 2 == 0 ? color1 : color2;

            var newNotePrefab = Instantiate(UI.NoteDataPrefab, UI.ListRect);
            noteDatas.Add(newNotePrefab);

            newNotePrefab.UpdateInfo(note.Value, color);

            newNotePrefab.Rect.anchoredPosition = new Vector2(0, height);
            height -= newNotePrefab.Rect.sizeDelta.y;

            UI.ListRect.sizeDelta = new Vector2(UI.ListRect.sizeDelta.x, height * -1);

            index++;
        }
    }

    public void CloseNote(bool playSFX)
    {
        if(playSFX)
        {
            PlaySound(closeNoteSFX[UnityEngine.Random.Range(0, closeNoteSFX.Length)]);
        }

        UpdateCanvasGroup(false, UI.NoteCanvasGroup);
        OnNoteClose();
    }
    private void UpdateSubscript()
    {
        UI.Subscript.text = readSubscript ? activePage.Text : string.Empty;
    }

    public void Next()
    {
        PlaySound(turnPageSFXs);
        currentPage++;

        DisplayPage(currentPage);
    }
    public void Previous()
    {
        PlaySound(turnPageSFXs);
        currentPage--;

        DisplayPage(currentPage);
    }
    public void Read()
    {
        readSubscript = !readSubscript;

        UpdateSubscript();
        UpdateUI();
    }

    private void ClearList()
    {
        foreach(var note in noteDatas)
        {
            Destroy(note.gameObject);
        }
        noteDatas.Clear();
    }
    private void OnNoteClose()
    {
        activeNote = null;
        currentPage = 0;
        readSubscript = false;
        sources[1].Stop();
        if(!usingNotesSystem)
        {
            SwitchGameControls(true);
        }
    }
    private void UpdateCanvasGroup(bool state, CanvasGroup canvasGroup)
    {
        switch(state)
        {
            case true:
                canvasGroup.alpha = 1.0f;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
                break;
            case false:
                canvasGroup.alpha = 0.0f;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
                break;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if(clip)
        {
            sources[0].PlayOneShot(clip);
        }
    }
    private void PlaySound(AudioClip[] clips)
    {
        if(clips != null)
        {
            var sfx = clips[UnityEngine.Random.Range(0, clips.Length)];
            sources[0].PlayOneShot(sfx);
        }
    }

    public static void AddNote(string key, Note note)
    {
        if(Notes.ContainsKey(key) == false)
        {
            Notes.Add(key, note);
        }
    }
    public static Note GetNote(string key)
    {
        if(Notes.ContainsKey(key))
        {
            return Notes[key];
        }
        return null;
    }

#endregion
}
