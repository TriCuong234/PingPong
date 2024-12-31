using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button pvpBtn;
    public Button pveBtn;

    public Button exitBtn;
    public Button settingBtn;

    public GameObject settingPanel;
    void Start()
    {
        pvpBtn.onClick.AddListener(OnPvpClick);

        pveBtn.onClick.AddListener(OnPvEClick);

        exitBtn.onClick.AddListener(() => {
            Application.Quit();
        });

        settingBtn.onClick.AddListener(() => {
            settingPanel.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPvpClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        PlayerPrefs.SetInt("pvp", 1);
        PlayerPrefs.Save();
    }

    public void OnPvEClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        PlayerPrefs.SetInt("pvp", 0);
        PlayerPrefs.Save();
    }
}
