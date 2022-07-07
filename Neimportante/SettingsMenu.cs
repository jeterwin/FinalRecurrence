using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;
    List<string> options = new List<string>();
    private Dictionary<int, Resolution> _resolutions = new Dictionary<int, Resolution>();
    //public AudioMixer sfxmixer;
    //public AudioMixerGroup dialogue;
    [Space]
    [Header("Audio")]
    public AudioMixer audiomixer;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider DialogueSlider;
    public Slider Sensitivity;
    [Space]
    [Header("Graphics")]
    public Dropdown graphics;
    public Dropdown resolutionDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown shadowsDropdown;
    public Dropdown textureDropdown;
    public Toggle fullscreen;
    public Toggle subtitles;
    [Space]
    [Header("FX")]
    public Toggle vignetteToggle;
    public Toggle motionBlurToggle;
    public Toggle grainToggle;
    public Toggle vsyncToggle;
    public Toggle bloomToggle;
    [Space]


    private int countRes = 0;

    public PostProcessProfile profile;
    public PostProcessProfile DepthOfFieldProfile;
    private Vignette vignette;
    private MotionBlur motionBlur;
    private Grain grain;
    private Bloom bloom;
    public bool subDec = true;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI subtitleText1;
    Resolution[] resolutions;

        private void Awake()
    {
        instance = this;
        //Quality
        //If there was a quality level saved, restore its settings based on its preset
        if (PlayerPrefs.HasKey("qualityLevel"))
        {
        switch(PlayerPrefs.GetInt("qualityLevel"))
        {
            case 0:
                {
                    textureDropdown.value = 0; // Full res
                    //PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 0; //Disabled
                    QualitySettings.antiAliasing = 0;
                    //PlayerPrefs.SetInt("antiAliasing", 0);

                    shadowsDropdown.value = 0;
                    //PlayerPrefs.SetInt("shadows", 0);
                    ChangeShadows(ShadowmaskMode.Shadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0); // Low
                    QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
                }
                break;
            case 1:
                {
                    textureDropdown.value = 0; // Full res
                    //PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 0; //Disabled
                    //PlayerPrefs.SetInt("antiAliasing", 0);
                    QualitySettings.antiAliasing = 0;

                    shadowsDropdown.value = 1;
                    //PlayerPrefs.SetInt("shadows", 1);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0); // Low
                    QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
                }
                break;
            case 2:
                {
                    textureDropdown.value = 0; // Full res
                    //PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 1; // 2x
                    //PlayerPrefs.SetInt("antiAliasing", 1);
                    QualitySettings.antiAliasing = 2;

                    shadowsDropdown.value = 2;
                    //PlayerPrefs.SetInt("shadows", 2);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.Medium, ShadowProjection.StableFit, 100, 3, 2); // Low
                    QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
                }
                break;
            case 3:
                {
                    textureDropdown.value = 0; // Full res
                    //PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 2; // 4x
                    //PlayerPrefs.SetInt("antiAliasing", 2);
                    QualitySettings.antiAliasing = 4;

                    shadowsDropdown.value = 3;
                    //PlayerPrefs.SetInt("shadows", 3);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.High, ShadowProjection.StableFit, 100, 3, 2); // Low
                    QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
                }
                break;
            case 4:
                {
                    textureDropdown.value = 0; // Full res
                    //PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 3; // 8x
                    //PlayerPrefs.SetInt("antiAliasing", 3);
                    QualitySettings.antiAliasing = 8;

                    shadowsDropdown.value = 4;
                    //PlayerPrefs.SetInt("shadows", 4);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.VeryHigh, ShadowProjection.StableFit, 100, 3, 4); // Low

                    QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
                }
                break;
        }
            //QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
            graphics.value = PlayerPrefs.GetInt("qualityLevel");
        }
        //Subtitles
        if(PlayerPrefs.GetInt("subtitles", 1) != 0)
        {
            subtitles.isOn = true;
            subDec = true;
            if(subtitleText != null)
                subtitleText.enabled = true;
            if(subtitleText1 != null)
                subtitleText1.enabled = true;
        }
        else
        {
            subtitles.isOn = false;
            subDec = false;
            if (subtitleText != null)
                subtitleText.enabled = false;
            if (subtitleText1 != null)
                subtitleText1.enabled = false;
        }
        //Sensitivity
        Sensitivity.value = PlayerPrefs.GetFloat("sens", 2f);
        if (Fps_Script.instance)
            Fps_Script.instance.mouseLook.XSensitivity = Fps_Script.instance.mouseLook.YSensitivity = PlayerPrefs.GetFloat("sens", 1f);
        //Fps_Script.instance.lookSpeed = PlayerPrefs.GetFloat("sens", 1f);
        //Vignette
        if (PlayerPrefs.HasKey("isVignette"))
        {
            if (PlayerPrefs.GetFloat("isVignette") != 0)
            {
            vignetteToggle.isOn = true;
            vignette = profile.GetSetting<Vignette>();
            vignette.intensity.value = 0.45f;
            }
            else
            {
            vignetteToggle.isOn = false;
            vignette = profile.GetSetting<Vignette>();
            vignette.intensity.value = 0f;
            }
        }

        //Bloom
        if (PlayerPrefs.HasKey("isBloom"))
        {
            bloom = profile.GetSetting<Bloom>();
            if (PlayerPrefs.GetInt("isBloom", 0) == 1)
                bloom.enabled.value = true;
            else
                bloom.enabled.value = false;
        }
        //Motion blur
        if (PlayerPrefs.HasKey("motionBlurOn"))
        {
            if (PlayerPrefs.GetInt("motionBlurOn") == 1)
            {
            motionBlurToggle.isOn = true;
            motionBlur = profile.GetSetting<MotionBlur>();
            motionBlur.active = true;
            }
            else
            {
            motionBlurToggle.isOn = false;
            motionBlur = profile.GetSetting<MotionBlur>();
            motionBlur.active = false;
            }
        }
        //Film grain
        if(PlayerPrefs.HasKey("filmGrain"))
        {
            if (PlayerPrefs.GetFloat("filmGrain") != 0)
            {
            grainToggle.isOn = true;
            grain = profile.GetSetting<Grain>();
            grain.intensity.value = 0.5f;
            }
            else
            {
            grainToggle.isOn = false;
            grain = profile.GetSetting<Grain>();
            grain.intensity.value = 0f;
            }
        }
        //Main volume
        if(PlayerPrefs.HasKey("volume"))
        {
            audiomixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volume")) * 20);
            musicSlider.value = PlayerPrefs.GetFloat("volume");
        }
        //SFX volume
        if (PlayerPrefs.HasKey("SFXvolume"))
        {
            audiomixer.SetFloat("SFXvolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXvolume")) * 20);
            SFXSlider.value = PlayerPrefs.GetFloat("SFXvolume");
        }
        if (PlayerPrefs.HasKey("Dialoguevolume"))
        {
            audiomixer.SetFloat("Dialoguevolume", Mathf.Log10(PlayerPrefs.GetFloat("Dialoguevolume")) * 20);
            DialogueSlider.value = PlayerPrefs.GetFloat("Dialoguevolume");
        }
        //Fullscreen
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            if(PlayerPrefs.GetInt("isFullscreen") == 1)
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
        //Vsync
        if (PlayerPrefs.HasKey("vsync"))
        {
            if(PlayerPrefs.GetInt("vsync") == 1)
            {
                QualitySettings.vSyncCount = 1;
                vsyncToggle.isOn = true;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                vsyncToggle.isOn = false;
            }
        }
        //AntiAliasing
        if (PlayerPrefs.HasKey("antiAliasing"))
        {
            if(PlayerPrefs.GetInt("antiAliasing") == 0)
            {
                antialiasingDropdown.value = 0;
                QualitySettings.antiAliasing = 0;
            }

            else if(PlayerPrefs.GetInt("antiAliasing") == 1)
            {
                antialiasingDropdown.value = 1;
                QualitySettings.antiAliasing = 2;
            }

            else if(PlayerPrefs.GetInt("antiAliasing") == 2)
            {
                antialiasingDropdown.value = 2;
                QualitySettings.antiAliasing = 4;
            }

            else if(PlayerPrefs.GetInt("antiAliasing") == 3)
            {
                antialiasingDropdown.value = 3;
                QualitySettings.antiAliasing = 8;
            }
        }
        //Shadows
        if (PlayerPrefs.HasKey("shadows"))
        {
            if(PlayerPrefs.GetInt("shadows") == 0)
            {
                ChangeShadows(ShadowmaskMode.Shadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0);
                shadowsDropdown.value = 0;
            }
            else if(PlayerPrefs.GetInt("shadows") == 1)
            {
                ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 2);
                shadowsDropdown.value = 1;
            }
            
            else if(PlayerPrefs.GetInt("shadows") == 2)
            {
                ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.Medium, ShadowProjection.StableFit, 100, 3, 2);
                shadowsDropdown.value = 2;
            }  
            else if(PlayerPrefs.GetInt("shadows") == 3)
            {
                ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.High, ShadowProjection.StableFit, 100, 3, 2);
                shadowsDropdown.value = 3;
            }
            else
            {
                ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.VeryHigh, ShadowProjection.StableFit, 100, 3, 4);
                shadowsDropdown.value = 4;
            }
        }
        //Texture
        if (PlayerPrefs.HasKey("texture"))
        {
            if(PlayerPrefs.GetInt("texture") == 0)
            {
                QualitySettings.masterTextureLimit = 0; // Quarter res
                textureDropdown.value = 0;
            }
            else if(PlayerPrefs.GetInt("texture") == 1)
            {
                QualitySettings.masterTextureLimit = 1; // Half res
                textureDropdown.value = 1;
            }
            else if(PlayerPrefs.GetInt("texture") == 2)
            {
                QualitySettings.masterTextureLimit = 2; // Full res
                textureDropdown.value = 2;
            }
        }
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        //List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = resolutions.Length - 1; i > 0; i--)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            if(resolutions[i].height >= 720 && resolutions[i].height != 990)
            {
                options.Add(option);
                _resolutions.Add(options.Count - 1, resolutions[i]);
                countRes++;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = options.FindIndex(c => c.Contains(Screen.currentResolution.height.ToString()) && c.Contains(Screen.currentResolution.width.ToString()) && c.Contains(Screen.currentResolution.refreshRate.ToString()));
        for (int i = countRes - 1; i >= 0; i--)
        {
            //resolutionDropdown.options.Add(new Dropdown.OptionData(options[i]));
            //resolutionDropdown.AddOptions(options);
            if (resolutions[i].width == Screen.currentResolution.width &&
    resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //resolutionDropdown.AddOptions(options);
        if(PlayerPrefs.HasKey("resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
            Resolution resolution = _resolutions[PlayerPrefs.GetInt("resolution")];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
        resolutionDropdown.value = currentResolutionIndex;
        Resolution resolution = _resolutions[currentResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        PlayerPrefs.Save();
    }
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float volume)
    {
        audiomixer.SetFloat("SFXvolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXvolume", volume);
        PlayerPrefs.Save();
    }
    public void SetDialogueVolume(float volume)
    {
        audiomixer.SetFloat("Dialoguevolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Dialoguevolume", volume);
        PlayerPrefs.Save();
    }
    public void SetQuality(int qualityIndex)
    {
        switch(qualityIndex)
        {
            case 0:
                {
                    QualitySettings.SetQualityLevel(qualityIndex);
                    textureDropdown.value = 0; // Full res
                    PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 0; //Disabled
                    QualitySettings.antiAliasing = 0;
                    PlayerPrefs.SetInt("antiAliasing", 0);

                    shadowsDropdown.value = 0;
                    PlayerPrefs.SetInt("shadows", 0);
                    ChangeShadows(ShadowmaskMode.Shadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0); // Low

                }
                break;
            case 1:
                {
                    QualitySettings.SetQualityLevel(qualityIndex);
                    textureDropdown.value = 0; // Full res
                    PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 0; //Disabled
                    QualitySettings.antiAliasing = 0;
                    PlayerPrefs.SetInt("antiAliasing", 0);

                    shadowsDropdown.value = 1;
                    PlayerPrefs.SetInt("shadows", 1);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0); // Low

                }
                break;
            case 2:
                {
                    QualitySettings.SetQualityLevel(qualityIndex);
                    textureDropdown.value = 0; // Full res
                    PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 1; // 2x
                    PlayerPrefs.SetInt("antiAliasing", 1);
                    QualitySettings.antiAliasing = 2;

                    shadowsDropdown.value = 2;
                    PlayerPrefs.SetInt("shadows", 2);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.Medium, ShadowProjection.StableFit, 100, 3, 2); // Low

                }
                break;
            case 3:
                {
                    QualitySettings.SetQualityLevel(qualityIndex);
                    textureDropdown.value = 0; // Full res
                    PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 2; // 4x
                    PlayerPrefs.SetInt("antiAliasing", 2);
                    QualitySettings.antiAliasing = 4;

                    shadowsDropdown.value = 3;
                    PlayerPrefs.SetInt("shadows", 3);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.High, ShadowProjection.StableFit, 100, 3, 2); // Low


                }
                break;
            case 4:
                {
                    QualitySettings.SetQualityLevel(qualityIndex);
                    textureDropdown.value = 0; // Full res
                    PlayerPrefs.SetInt("texture", 0);
                    QualitySettings.masterTextureLimit = 0; // Full res

                    antialiasingDropdown.value = 3; // 8x
                    PlayerPrefs.SetInt("antiAliasing", 3);
                    QualitySettings.antiAliasing = 8;

                    shadowsDropdown.value = 4;
                    PlayerPrefs.SetInt("shadows", 4);
                    ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.VeryHigh, ShadowProjection.StableFit, 100, 3, 4); // Low
                }
                break;
        }
        //QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }
    public void setVsync(bool on)
    {
        if(on == true)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("vsync", 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vsync", 0);
        }
        PlayerPrefs.Save();
    }
    public void setTextureQuality(int index2)
    {
        if(index2 == 0)
        {
        QualitySettings.masterTextureLimit = 0; // Full res
        PlayerPrefs.SetInt("texture", 0);
        }
        else if(index2 == 1)
        {
        QualitySettings.masterTextureLimit = 1; // Half res
        PlayerPrefs.SetInt("texture", 1);
        }

        else if(index2 == 2)
        {
        QualitySettings.masterTextureLimit = 2; // Quarter res
        PlayerPrefs.SetInt("texture", 2);
        }
        PlayerPrefs.Save();
    }
    public void SetShadows(int index1)
    {
        if(index1 == 0)
        {
            ChangeShadows(ShadowmaskMode.Shadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0);
            PlayerPrefs.SetInt("shadows", 0);
        }
        else if(index1 == 1)
        {
            ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 100, 3, 0);
            PlayerPrefs.SetInt("shadows", 1);
        }
           
        else if(index1 == 2)
        {
            ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.Medium, ShadowProjection.StableFit, 100, 3, 2);
            PlayerPrefs.SetInt("shadows", 2);
        }
            
        else if(index1 == 3)
        {
            ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.High, ShadowProjection.StableFit, 100, 3, 2);
            PlayerPrefs.SetInt("shadows", 3);
        }  
        else
        {
            ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.VeryHigh, ShadowProjection.StableFit, 100, 3, 4);
            PlayerPrefs.SetInt("shadows", 4);
        }
        PlayerPrefs.Save();
    }
    void ChangeShadows(ShadowmaskMode mask, ShadowQuality quality, ShadowResolution res, ShadowProjection projection, float shadowDistance, float shadowNearPlaneOffset, int shadowCascades)
    {
        QualitySettings.shadowmaskMode = mask;
        QualitySettings.shadows = quality;
        QualitySettings.shadowResolution = res;
        QualitySettings.shadowProjection = projection;
        QualitySettings.shadowDistance = shadowDistance;
        QualitySettings.shadowNearPlaneOffset = shadowNearPlaneOffset;
        QualitySettings.shadowCascades = shadowCascades;
    }
    public void SetVignette(bool isOn)
    {
        vignette = profile.GetSetting<Vignette>();
        if(isOn == true)
        {
            vignette.intensity.value = 0.45f;
            PlayerPrefs.SetFloat("isVignette", 0.45f);
        }
        else
        {
            vignette.intensity.value = 0f;
            PlayerPrefs.SetFloat("isVignette", 0f);
        }
        PlayerPrefs.Save();
    }
    public void SetBloom(bool isBloomOn)
    {
        bloom = profile.GetSetting<Bloom>();
        if (isBloomOn == true)
        {
            bloom.enabled.value = true;
            PlayerPrefs.SetInt("isBloom", 1);
        }
        else
        {
            bloom.enabled.value = false;
            PlayerPrefs.SetInt("isBloom", 0);
        }
        PlayerPrefs.Save();
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if(isFullscreen == true)
        {
        PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else
        {
        PlayerPrefs.SetInt("isFullscreen", 0);
        }
        PlayerPrefs.Save();
    }
    public void antiAliasing(int index)
    {
        if(index == 0)
        {
            PlayerPrefs.SetInt("antiAliasing", 0);
            QualitySettings.antiAliasing = 0;
        }

        else if(index == 1)
        {
            PlayerPrefs.SetInt("antiAliasing", 1);
            QualitySettings.antiAliasing = 2;
        }

        else if(index == 2)
        {
            PlayerPrefs.SetInt("antiAliasing", 2);
            QualitySettings.antiAliasing = 4;
        }

        else if(index == 3)
        {
            PlayerPrefs.SetInt("antiAliasing", 3);
            QualitySettings.antiAliasing = 8;
        }
        PlayerPrefs.Save();
    }
    public void SetMotionBlur(bool isTurnedOn)
    {
        motionBlur = profile.GetSetting<MotionBlur>();
        if(isTurnedOn == true)
        {
            motionBlur.active = true;
            PlayerPrefs.SetInt("motionBlurOn", 1);
        }
        else
        {
            motionBlur.active = false;
            PlayerPrefs.SetInt("motionBlurOn", 0);
        }
        PlayerPrefs.Save();
    }
        public void SetFilmGrain(bool lmao)
    {
        grain = profile.GetSetting<Grain>();
        if(lmao == true)
        {
            grain.intensity.value = 0.5f;
            PlayerPrefs.SetFloat("filmGrain", 0.5f);
        }
        else
        {
            grain.intensity.value = 0f;
            PlayerPrefs.SetFloat("filmGrain", 0);
        }
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float sens)
    {
        
        PlayerPrefs.SetFloat("sens", sens);
        if(Fps_Script.instance)
            Fps_Script.instance.mouseLook.XSensitivity = Fps_Script.instance.mouseLook.YSensitivity = sens;
        //Fps_Script.instance.lookSpeed = sens;
        PlayerPrefs.Save();
    }

    public void SetSubtitles(bool subtitles)
    {
        if(subtitles == true)
        { 
        PlayerPrefs.SetInt("subtitles", 1);
        //if(Subtitles.instance)
            //{
            subDec = true;
            subtitleText.enabled = true;
            //Subtitles.instance.textBox.enabled = true;
            //}
        }
        else
        {
        PlayerPrefs.SetInt("subtitles", 0);
            //if(Subtitles.instance)
            //{
            subDec = false;
            subtitleText.enabled = false;
            //Subtitles.instance.textBox.enabled = false;
            //}
        }
        PlayerPrefs.Save();
    }

}
