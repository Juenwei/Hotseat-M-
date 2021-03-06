using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickAudio;
    public AudioMixer audioMixer;
    [SerializeField] private GameObject startMenu,settingMenu;

    private void Start()
    {
        clickAudio = GetComponent<AudioSource>();
    }

    public void OpenDifficultMenu()
    {
        clickAudio.Play();
        startMenu.SetActive(true);
    }

    public void CloseDifficultMenu()
    {
        clickAudio.Play();
        startMenu.SetActive(false);
    }

    public void quitGame()
    {
        
        Application.Quit();
    }

    public void UIEnabled()
    {
        GameObject.Find("Canvas/mainMenu/UI").SetActive(true);
    }

    public void OpenSettingMenu()
    {
        clickAudio.Play();
        settingMenu.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void CloseSettingMenu()
    {
        clickAudio.Play();
        settingMenu.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }

    public void SelectEasy()
    {
        GameManager.instance.PickDifficulty(0);
    }

    public void SelectNormal()
    {
        GameManager.instance.PickDifficulty(1);
    }

    public void SelectHard()
    {
        GameManager.instance.PickDifficulty(2);
    }
    
}
