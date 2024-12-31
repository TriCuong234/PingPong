using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public Slider generalVolumeSlider;
    public Slider musicVolumeSlider;

    public Slider sfxVolumeSlider;

    public Button ctnBtn;

    public Button exitBtn;
    public GameObject settingPanel;

    public AudioSource bgmSource;

    public AudioSource sfxSource;

    public static SettingsController instance;

    public AudioMixer generalMixer;
    void Awake()
    {
        generalMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("GeneralVolume", 0.5f));
        generalMixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
        generalMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        musicVolumeSlider.onValueChanged.AddListener(BgmValue);
        sfxVolumeSlider.onValueChanged.AddListener(SfxValue);
        generalVolumeSlider.onValueChanged.AddListener(GeneralVolume);
        exitBtn.onClick.AddListener(Exit);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    // Hàm điều chỉnh âm lượng và lưu giá trị vào PlayerPrefs
    void Continue()
    {

    }

    void Exit()
    {
        settingPanel.gameObject.SetActive(false);
    }

    void BgmValue(float volume)
    {
        generalMixer.SetFloat("BGMVolume", volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    void SfxValue(float volume)
    {
        generalMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    void GeneralVolume(float volume)
    {
        generalMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("GeneralVolume", volume);
        PlayerPrefs.Save();
    }
}



