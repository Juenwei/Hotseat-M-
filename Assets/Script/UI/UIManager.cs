using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject pauseMenu, wallRunTip, movementTip, deathPanel;
    [SerializeField] private GameObject cursorLockObject;

    //private bool isPauseMenuOpen;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

    }

    public void ShowWallRunTip()
    {
        movementTip.SetActive(false);
        wallRunTip.SetActive(true);

    }

    public void ShowMovemntTip()
    {
        movementTip.SetActive(true);
        wallRunTip.SetActive(false);
    }
  
    public void ShowDeathPanel(bool permission)
    {
        deathPanel.SetActive(permission);
    }
   
}
