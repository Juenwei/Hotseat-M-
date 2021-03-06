using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, restartMenu, backToMainMenu;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private CursorLock cursorLockObject;

    private void Start()
    {
        clickAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetPlayerInput();
    }

    void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLockObject.GetComponent<CursorLock>().CursorVisibleSetup(false);
            PauseGame();

        }
    }

    public void PauseGame()
    {
        clickAudio.Play();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {

        clickAudio.Play();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        cursorLockObject.GetComponent<CursorLock>().CursorVisibleSetup(true);
        
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }

    public void OpenRestartWarning()
    {
        clickAudio.Play();
        restartMenu.SetActive(true);
        pauseMenu.SetActive(false);
        
    }

    public void OpenBackToMenuWarning()
    {
        clickAudio.Play();
        backToMainMenu.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void CloseRestarWarning()
    {
        clickAudio.Play();
        restartMenu.SetActive(false);
        pauseMenu.SetActive(true);
        
    }

    
    public void CloseBackToMenuWarning()
    {
        clickAudio.Play();
        backToMainMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ConfirmToRestart()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
